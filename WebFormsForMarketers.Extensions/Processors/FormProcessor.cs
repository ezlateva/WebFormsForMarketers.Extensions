using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Controls.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Submit;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Submit;
using Sitecore.Forms.Core.Data;
using Sitecore.Reflection;

namespace WebFormsForMarketers.Extensions.Processors
{
    public class FormProcessor
    {
        public FormProcessorResult Process(FormData data)
        {
            FormProcessorResult result = new FormProcessorResult();

            if (string.IsNullOrEmpty(data.FormId))
            {
                result.Success = false;
                result.ResultMessage = "Invalid Form Id";
            }
            else
            {
                bool failed = false;

                ID formId = new ID(data.FormId);
                FormItem formItem = FormItem.GetForm(formId);

                if (formItem != null)
                {
                    //Get form fields of the WFFM
                    FieldItem[] formFields = formItem.FieldItems;

                    //Create collection of fields
                    List<AdaptedControlResult> adaptedFields = new List<AdaptedControlResult>();
                    foreach (FormField field in data.Fields)
                    {
                        FieldItem formFieldItem = formFields.FirstOrDefault(x => x.Name == field.FieldName);
                        if (formFieldItem != null)
                        {
                            adaptedFields.Add(GetControlResult(field.FieldValue, formFieldItem));
                        }
                        else
                        {
                            // log and bail out
                            Log.Warn(string.Format("Field Item {0} not found for form with ID {1}", field.FieldName, data.FormId), this);
                            failed = true;
                            result.Success = false;
                            result.ResultMessage = string.Format("Invalid field name: {0}", field.FieldName);
                            break;
                        }
                    }

                    if (!failed)
                    {
                        // Get form action definitions
                        List<ActionDefinition> actionDefinitions = new List<ActionDefinition>();
                        ListDefinition definition = formItem.ActionsDefinition;
                        if (definition.Groups.Count > 0 && definition.Groups[0].ListItems.Count > 0)
                        {
                            foreach (GroupDefinition group in definition.Groups)
                            {
                                foreach (ListItemDefinition item in group.ListItems)
                                {
                                    actionDefinitions.Add(new ActionDefinition(item.ItemID, item.Parameters)
                                                              {
                                                                  UniqueKey = item.Unicid
                                                              });
                                }
                            }
                        }

                        //Execute form actions
                        foreach (ActionDefinition actionDefinition in actionDefinitions)
                        {
                            try
                            {
                                ActionItem action = ActionItem.GetAction(actionDefinition.ActionID);
                                if (action != null)
                                {
                                    if (action.ActionType == ActionType.Save)
                                    {
                                        object saveAction = ReflectionUtil.CreateObject(action.Assembly, action.Class,
                                                                                        new object[0]);
                                        ReflectionUtils.SetXmlProperties(saveAction, actionDefinition.Paramaters, true);
                                        ReflectionUtils.SetXmlProperties(saveAction, action.GlobalParameters, true);
                                        if (saveAction is ISaveAction)
                                        {
                                            ((ISaveAction) saveAction).Execute(formId, adaptedFields, null);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // log and bail out
                                Log.Warn(ex.Message, ex, this);
                                result.Success = false;
                                result.ResultMessage = actionDefinition.GetFailureMessage();
                                failed = true;

                                break;
                            }

                        }

                        if (!failed)
                        {
                            // set successful result
                            result.Success = true;
                            result.ResultMessage = formItem.SuccessMessage;
                        }
                    }
                }
                else
                {
                    result.Success = false;
                    result.ResultMessage = "Form not found: invalid form Id";
                }
            }

            return result;
        }

        private AdaptedControlResult GetControlResult(string fieldValue, FieldItem fieldItem)
        {
            //Populate fields with values
            ControlResult controlResult = new ControlResult(fieldItem.Name, HttpUtility.UrlDecode(fieldValue), string.Empty)
            {
                FieldID = fieldItem.ID.ToString(),
                FieldName = fieldItem.Name,
                Value = HttpUtility.UrlDecode(fieldValue),
                Parameters = string.Empty
            };
            return new AdaptedControlResult(controlResult, true);
        }
    }
}
