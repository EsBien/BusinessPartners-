using BL_;
using DL;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebProject
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        } 
        public async Task Invoke(HttpContext httpContext)//,IRating_BL Rating_BL
        {
            Rating rating = new Rating();
            rating.RecordDate = DateTime.Now;
            rating.Host = httpContext.Request.Host.Value;
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path.Value;

            rating.Referer = httpContext.Request.Headers["Referer"];
            rating.UserAgent = httpContext.Request.Headers["User-Agent"];

        //rating.Referer = httpContext;m
            //rating.UserAgent = httpContext;

           // await Rating_BL.addRating(rating);

            await _next(httpContext);
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
