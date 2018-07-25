﻿namespace UniVerseDotNetCore.Models
{
    public class Contracts : CSSAccountFile
    {
        public override string FileName
        {
            get => this.GetType().Name;

            set => value = this.GetType().Name.ToUpperInvariant();
        }
    }
}