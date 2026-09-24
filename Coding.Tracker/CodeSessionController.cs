using System;
using System.Collections.Generic;
using System.Text;

namespace Coding.Tracker
{
    public class CodeSessionController
    {
        private readonly CodeSessionService service;
        private CodeSession? codeSession;
        private DateTime startTime;
        private DateTime endTime;

        public CodeSessionController(CodeSessionService service)
        {
            this.service = service;
        }

        public void StartSession()
        {
            startTime = DateTime.Now;
        }

        public void StopSession()
        {
            endTime = DateTime.Now;
            codeSession = new CodeSession
            {
                StartTime = startTime.ToString("dd-MM-yy HH:mm:ss"),
                EndTime = endTime.ToString("dd-MM-yy HH:mm:ss"),
                Duration = TimeOnly.FromTimeSpan(endTime - startTime).ToString("HH:mm:ss")
            };
            service.Create(codeSession);
        }
    }
}
