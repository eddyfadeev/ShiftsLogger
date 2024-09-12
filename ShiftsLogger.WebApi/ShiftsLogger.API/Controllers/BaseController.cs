using System.Data;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Controllers;

public abstract class BaseController<TEntity> : ControllerBase 
    where TEntity : class, IDbModel
{
    private readonly IUnitOfWork _unitOfWork;
    
    protected BaseController(IUnitOfWork unitOfWork) => 
        _unitOfWork = unitOfWork;
    
    private protected abstract int GetEntityId(TEntity entity);
    
    /// <summary>
    /// Fetches a specific entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity with the specified ID if found,
    /// or an error response if not found or if the request is invalid.</returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public abstract Task<IActionResult> GetEntryById(int id);

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public abstract Task<IActionResult> GetAllEntities();

    /// <summary>
    /// Adds a new entity to the system.
    /// </summary>
    /// <param name="entity">The entity object containing details about the entity to be added.</param>
    /// <returns>An action result indicating the outcome of the operation,
    /// including the created entity on success or an error response on failure.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddEntity([FromBody] TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            var errors = 
                ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            
            return BadRequest(errors);
        }
        
        try
        {
            await _unitOfWork.Repository<TEntity>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();
            
            int entityId = GetEntityId(entity);
            return CreatedAtAction(
                nameof(GetEntryById),
                new { id = entityId },
                entity);
        }
        catch (DataException dex)
        {
            ModelState.AddModelError("", dex.Message);
            return BadRequest($"Failed to add entity {nameof(TEntity)}");
        }
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">The ID of the entity to update.</param>
    /// <param name="entity">The entity object with updated details.</param>
    /// <returns>Returns OK if the update is successful;
    /// BadRequest if the ID does not match or the update fails.</returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEntity(int id, [FromBody] TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        int entityId = GetEntityId(entity);
        if (id != entityId)
        {
            return BadRequest($"Entity {nameof(TEntity)} ID does not match");
        }
        
        try
        {
            await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return Ok(entity);
        }
        catch (DataException dex)
        {
            ModelState.AddModelError("", dex.Message);
            return BadRequest($"Failed to update entity {nameof(TEntity)}");
        }
    }

    /// <summary>
    /// Deletes an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A status indicating the result of the operation.</returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteEntity(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound($"Entity {nameof(TEntity)} with ID: {id} not found");
            }
            
            await _unitOfWork.Repository<TEntity>().DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return Ok($"Entity {nameof(TEntity)} with ID: {id} was deleted.");

        }
        catch (DataException dex)
        {
            ModelState.AddModelError("", dex.Message);
            return BadRequest($"Failed to remove entity {nameof(TEntity)}");
        }
    }
}