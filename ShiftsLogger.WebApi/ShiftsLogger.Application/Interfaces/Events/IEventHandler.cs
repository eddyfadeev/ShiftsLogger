﻿using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces.Events;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent @event);
}