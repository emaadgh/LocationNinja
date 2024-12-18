﻿// Solution
global using LocationNinja.Common.Persistence;
global using LocationNinja.Extensions;
global using LocationNinja.Features.IpLocation;
global using LocationNinja.Features.IpLocation.Domain;
global using LocationNinja.Features.IpLocation.Endpoints;
global using LocationNinja.Common.Filters;
global using LocationNinja.Features.IpLocation.Models;
global using LocationNinja.Features.IpLocation.Providers.IpApi;
// Built-in
global using Microsoft.AspNetCore.Mvc;
global using System.Net;

// Third-party
global using AutoMapper;
global using FluentValidation;
global using Microsoft.EntityFrameworkCore;
global using MassTransit;
global using LocationNinja.IntegrationMessages.Internal;