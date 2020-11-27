﻿namespace Specshell.OmnicDde
{
    public class CommandsHandler
    {
        private readonly Dde dde;

        public CommandsHandler(Dde dde)
        {
            this.dde = dde;
        }


        /// <summary>
        /// This command retrieves version and copyright information for OMNIC and sets Result Current to About string. If invoked, the About Dialog box appears.
        /// </summary>
        public CommandResponse About()
        {
            return Execute("[About]", true);
        }

        /// <summary>
        /// This command adds the two selected spectra together to produce a new spectrum.
        /// 
        /// Throws exception if not exactly two spectra is selected.
        /// </summary>
        public CommandResponse Add()
        {
            return Execute("[Add]", true);
        }


        /// <summary>
        /// This command initiates a sample data collection.
        /// </summary>
        public CommandResponse CollectSample(string sampleTitle) 
        {
            return Execute("[CollectSample\"\"" + sampleTitle + "\"\"]", true);
        }


        /// <summary>
        /// This command moves all spectra from the hidden DDE window to the current active spectral window or the specified spectral window.
        /// </summary>
        public CommandResponse Display(string windowTitle = null) 
        {
            string command = windowTitle == null ? "[Display]" : "[Display " + windowTitle + "]";
            return Execute(command, true);
        }

        /// <summary>
        /// This command multiplies the entire spectrum by a factor and produces a new spectrum in the active spectral window.
        /// </summary>
        public CommandResponse Multiply(double factor)
        {
            return Execute("[Multiply " + factor + "]", true);
        }

        /// <summary>
        /// This command is not documented in the pdf.
        /// 
        /// This command performs an advanced atr correction on the current selected spectrum.
        /// 
        /// </summary>
        public CommandResponse AdvancedAtr(double crystalRefractiveIndex, double angleOfIncidenceDegrees, double numberOfBounces, double sampleRefractiveIndex)
        {
            return Execute("[AdvancedATR " + crystalRefractiveIndex + " " + angleOfIncidenceDegrees + " " + numberOfBounces + " " + sampleRefractiveIndex + "]", false, 5000);
        }


        /// <summary>
        /// This command saved the selected spectrum on a disk. If no filename is specified the operator will be prompted for a filename.
        /// 
        /// The given filename must contain the entire path and the filename with the file extension. The given file extension determines the file type exported. 
        /// </summary>
        public CommandResponse Export(string filename = null)
        {
            string command = filename == null ? "[Export]" : "[Export " + filename + "]";
            return Execute(command, true);
        }

        public CommandResponse SelectAll()
        {
            return Execute("[Select All]", true);
        }

        public CommandResponse DeleteSelectedSpectra()
        {
            return Execute("[DeleteSelectedSpectra]", true);
        }

        private CommandResponse Execute(string command, bool hasResultMessage, int timeOut = 500) 
        {
            try
            {
                dde.Execute(command, timeOut);
                string resultMessage = hasResultMessage ? dde.ResultCurrent : null;
                return new CommandResponse(true, resultMessage, null);
            }
            catch (NDde.DdeException e)
            {
                return new CommandResponse(false, null, e);
            }
        }
    }
}
