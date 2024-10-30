using System;

namespace TicketBrite.Core.Interfaces;

public interface IEventRepository
{
    public void CreateEvent();
    public void UpdateEvent();
    public void DeleteEvent();
    public void GetAllEvents();
    public void GetEventsByStatus();
}
