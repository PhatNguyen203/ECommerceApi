﻿using ECommerce.Api.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
