using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.Extensions;

public static class ControllerExtensions
{
    public static void SetPaginationMetadata(this ControllerBase contoller, PaginationMetaData metaData) =>
        contoller.HttpContext.Items[nameof(PaginationMetaData)] = metaData;
}