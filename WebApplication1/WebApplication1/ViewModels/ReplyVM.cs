using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class ReplyVM
    {
        public string Reply { get; set; } 
        //chatroom index 페이지에 있는 input name이랑 일치해야함

        public int CID { get; set; }
        //commentID
    }
}