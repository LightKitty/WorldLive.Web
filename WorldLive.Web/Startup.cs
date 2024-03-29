﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WorldLive.Core.Consts;

namespace WorldLive.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression(); //响应压缩
            services.AddResponseCaching(); //响应缓存
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCompression(); //响应压缩
            app.UseResponseCaching(); //响应缓存
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc((RouteBuilder) =>
            {
                RouteBuilder.MapRoute("Default", "{Controller}/{Action}/{Parameter}", new { @Controller = "Home", @Action = "Screenshots", @Parameter = string.Empty });
            });

            CommonConst.WebInit(env.WebRootPath);
        }
    }
}
