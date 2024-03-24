using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace program
{
    internal class UpdateForecast
    {
        WeatherLogic model = null;
        int sleep = 0;
        Task task = null;

        private void Update()
        {
            while (true)
            {
                task.Wait(sleep);
                model.Forecast(model.City);
            }
        }
        public UpdateForecast(WeatherLogic model, int sleepInMilisec)
        {
            this.model = model;
            this.sleep = sleepInMilisec;
            this.task = new Task(Update);
            task.Start();
        }
    }
}
