using InterviewTask.Models;
using InterviewTask.Services;
using System;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {

        private IHelperServiceRepository MyHelperServiceRespository;
        private ICustomLogger MyCustomLogger;

        [HttpGet]
        public ActionResult Index() {
            DateTime CurrentDateTime = DateTime.Now;
            MyCustomLogger = new CustomLogger();
            MyCustomLogger.Insert(Request.UserHostAddress,CurrentDateTime.ToString(), Request.Url.AbsolutePath);       
            int CurrentHour = CurrentDateTime.Hour;

            MyHelperServiceRespository = new HelperServiceRepository();
            var model = new IndexViewModel();
            model.HelperServicesModel = MyHelperServiceRespository.Get();

            foreach (HelperServiceModel service in model.HelperServicesModel) {
                try {
                    int[,] OpeningTimesArray = new int[,] {
                    {service.SundayOpeningHours[0], service.SundayOpeningHours[1]},
                    {service.MondayOpeningHours[0], service.MondayOpeningHours[1]},
                    {service.TuesdayOpeningHours[0], service.TuesdayOpeningHours[1]},
                    {service.WednesdayOpeningHours[0], service.WednesdayOpeningHours[1]},
                    {service.ThursdayOpeningHours[0], service.ThursdayOpeningHours[1]},
                    {service.FridayOpeningHours[0], service.FridayOpeningHours[1]},
                    {service.SaturdayOpeningHours[0], service.SaturdayOpeningHours[1]},
                };

                    for (int i = (int)CurrentDateTime.DayOfWeek; i < OpeningTimesArray.Length;) {
                        if (i == (int)CurrentDateTime.DayOfWeek) {
                            if (CurrentHour > OpeningTimesArray[i, 0] && CurrentHour < OpeningTimesArray[i, 1]) {
                                service.StatusMessage = "OPEN TODAY UNTIL - " + OpeningTimesArray[i, 1] + ":00";
                                service.StatusColour = "bg-color-donation-orange";
                                break;
                            }
                        } else {
                            if (OpeningTimesArray[i, 0] != 0) {
                                service.StatusMessage = "CLOSED - REOPENS " + Enum.GetName(typeof(DayOfWeek), i).ToUpper() + " at " + OpeningTimesArray[i, 1] + ":00";
                                service.StatusColour = "bg-color-light-grey";
                                break;
                            }
                        }
                        if (i == 6) {
                            i = 0;
                        } else {
                            i++;
                        }
                    }
                } catch (Exception e) {
                    service.StatusMessage = "We're sorry, we are temporarily unable to display these opening times";
                    service.StatusColour = "bg-color-light-grey";              
                    MyCustomLogger.Insert(Request.UserHostAddress, CurrentDateTime.ToString(), Request.Url.AbsolutePath, e.Message);
                }

        }
            return View(model);
        }


    }

}