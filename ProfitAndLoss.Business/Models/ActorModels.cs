using ProfitAndLoss.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Models
{
    // create
    public class RequestCreateActorModel : Mapping<Actor>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    // update
    public class RequestUpdateActorModel
    {
        // field
    }
    // get select
    // api/Actor/select
    // lay ra list cacs ResponseSelectActorModel
    public class ResponseSelectActorModel
    {
        //id
        //name
    }
    // get only
    // api/Actor/only
    // lay ra list cacs ResponseOnlyActorModel
    public class ResponseOnlyActorModel : Mapping<Actor>
    {
        //id
        //name
        //ShortDescription
        //Description
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    // get all
    // api/Actor/all
    // lay ra list cacs ResponseOnlyActorModel
    public class ResponseAllActorModel
    {
        //id
        //name
        //ShortDescription
        //Description
        //List<ResponseSelectCertificationModel> list = new ...
    }
}
