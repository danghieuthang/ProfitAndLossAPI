
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Helpers
{
    // create
    public class RequestCreateSkillModel
    {
        // field
    }
    // update
    public class RequestUpdateSkillModel
    {
        // field
    }
    // get select
    // api/skill/select
    // lay ra list cacs ResponseSelectSkillModel
    public class ResponseSelectSkillModel
    {
        //id
        //name
    }
    // get only
    // api/skill/only
    // lay ra list cacs ResponseOnlySkillModel
    public class ResponseOnlySkillModel
    {
        //id
        //name
        //ShortDescription
        //Description
    }
    // get all
    // api/skill/all
    // lay ra list cacs ResponseOnlySkillModel
    public class ResponseAllSkillModel
    {
        //id
        //name
        //ShortDescription
        //Description
        //List<ResponseSelectCertificationModel> list = new ...
    }
}
