﻿namespace Specshell.Omnic.Dde.Commands
{
    public class Multiply : ICommand
    {
        private readonly double _factor;

        public Multiply(double factor)
        {
            _factor = factor;
        }

        public string Command => $"[Multiply {_factor}]";

        public CommandType Type => CommandType.Execute;
    }
}