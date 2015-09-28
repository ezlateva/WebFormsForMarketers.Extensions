using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebFormsForMarketers.Extensions.Client.Marketing;
using WebFormsForMarketers.Extensions.Processors;

namespace WebFormsForMarketers.Extensions.Client
{
    public class WffmRemoteService : IMarketingService
    {
        private string RemoteUrl { get; private set; }

        public WffmRemoteService(string remoteUrl)
        {
            RemoteUrl = remoteUrl;
        }

        public void DoSomething(MarketingData data)
        {
            var formFields = new List<FormField>();
            formFields.Add(new FormField
            {
                FieldName = "First Name",
                FieldValue = data.FirstName
            });
            formFields.Add(new FormField
            {
                FieldName = "Last Name",
                FieldValue = data.LastName
            });
            formFields.Add(new FormField
            {
                FieldName = "Email",
                FieldValue = data.Email
            });

            FormData formData = new FormData();
            formData.FormId = MY_WFFM_FORM_ITEM_ID;
            formData.Fields = formFields;


            using (WebClient client = new WebClient())
            {
                client.UploadValues(RemoteUrl, "POST", formData);
            }
        }
    }
}
