using Flashcards.Controllers;
using Flashcards.DTOs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards
{
    public abstract class Command
    {
        public abstract bool Execute();
    }
}
