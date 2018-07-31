﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;

namespace UniVerseDotNetCore.Models
{
    public class ChangeItRequest<T> where T : IChangeIt2, new()
    {


        public ChangeItRequest()
        {
            
        }
        public ChangeItRequest(string changeType, AccountList getList, CssCredentialsModel credentials, CSSAccountFile file,Note note)
        {
            TypeOfChange = changeType;
            GetListName = getList;
            Credentials = credentials;
            File = file;
            Note = note;
        }

        public T NewCode  { get; set; }
        public string TypeOfChange { get; set; }

        public AccountList GetListName { get; set; }
        public CssCredentialsModel Credentials { get; set; }
        public CSSAccountFile File { get; set; }
        public Note Note { get; set; }
    }

  
}
