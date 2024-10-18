﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface ITicketRepository
    {
        public void CreateTicket(EventTicket ticket);
        public List<EventTicket> GetTicketsOfEvent(Guid eventID);
        public EventTicket GetTicketByID(Guid ticketID);
    }
}
