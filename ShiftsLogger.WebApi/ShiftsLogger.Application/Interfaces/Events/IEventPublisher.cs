﻿using ShiftsLogger.Domain.Interfaces;

namespace ShiftsLogger.Application.Interfaces.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}