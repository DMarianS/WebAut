/*
 * Created by Ranorex
 * User: mserbu
 * Date: 6/19/2021
 * Time: 7:41 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Linq;
using Ranorex;
using Ranorex.Core.Testing;
using System.Collections.Generic;

namespace AutW.Lib
{
	/// <summary>
	/// Description of Way2Automation.
	/// </summary>
	[UserCodeCollection]
	public static class Way2Automation
	{
		#region Methods
		//[UserCodeMethod]
		public static int OpenBrowserAndNavigateToPage()
		{		
			string url = "http://way2automation.com/way2auto_jquery/index.php";
			Report.Log(ReportLevel.Always,"Open Chrome browser and navigate to page: " + url);
			return Host.Current.OpenBrowser(url, "Chrome", "", false, false, false, false, false, true);		
		}
		
		//[UserCodeMethod]
		public static void CloseBrowser(int processId)
		{
			Report.Log(ReportLevel.Always,"Int retrieved from OpenBrowswer method: " + processId.ToString());
			Report.Log(ReportLevel.Always,"Trying to close the Chrome tab...");
			//Host.Current.CloseApplication(processId);
		}
		

		
		#endregion
		
		#region TestCases
		[UserCodeMethod]
		public static void TC_DatePicker()
		{
			int processId =  OpenBrowserAndNavigateToPage();
			try 
			{
				
				var repository = Way2AutomationRepository.Instance;
				Report.Log(ReportLevel.Always,"Trying to click EnterToTheTestingWebsite link...");
				repository.WelcomeToTheTestSite.EnterToTheTestingWebsite.Click();
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Select the DatePicker widget.");
				repository.WelcomeToTheTestSite.DatePickerLink.Click();
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Go to Format Date section.");
				repository.WelcomeToTheTestSite.FormatDate.Click();
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Open Date Picker...");
				repository.WelcomeToTheTestSite.DatePickerSection.Datepicker.Click();
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Trying to find the highlighted day in the table...");
				//the text box / input element
				InputTag dateInputTag = repository.WelcomeToTheTestSite.DatePickerSection.Datepicker;
				
				DivTag dateDivTag = repository.WelcomeToTheTestSite.DatePickerSection.DatePickerDiv;
				List<ATag> listOfDaysFromDatePicker = dateDivTag.FindDescendants<ATag>().ToList();
				ATag highlightedDay = listOfDaysFromDatePicker.Where(o=>o.GetAttributeValue<string>("Class").Contains("ui-state-highlight")).FirstOrDefault();					
				Report.Log(ReportLevel.Always,"Day highlighted: " + highlightedDay.InnerText);	
				Report.Log(ReportLevel.Always,"Proceed with selecting day...");				
				highlightedDay.Click();
								
				string dateBeforeChangingFormat = dateInputTag.TagValue;
				//string dateBeforeChangingFormat = repository.WelcomeToTheTestSite.HttpWay2automationComWay2autoJquer.Datepicker.InnerText;				
			    Report.Log(ReportLevel.Always,"Date value from text box before changing format: " + dateBeforeChangingFormat);			
				
			    
			    Report.Log(ReportLevel.Always,"Proceed with changing format...");			    
			    SelectTag formatSelectTag = repository.WelcomeToTheTestSite.DatePickerSection.Format;
			    List<OptionTag> listOfOptions = formatSelectTag.FindDescendants<OptionTag>().ToList();
			    OptionTag ourOption = listOfOptions.Where(o=>o.GetAttributeValue<string>("InnerText").Contains("ISO 8601 - yy-mm-dd")).FirstOrDefault();
			    ourOption.Select();
			    
			    Report.Log(ReportLevel.Always,"Option value from text box: " + formatSelectTag.TagValue);
			    //Report.Log(ReportLevel.Always,"Option value from text box: " + repository.WelcomeToTheTestSite.HttpWay2automationComWay2autoJquer.Format.InnerText);			
				
			    string dateAfterChangingFormat = dateInputTag.TagValue;
			    //string dateAfterChangingFormat = repository.WelcomeToTheTestSite.HttpWay2automationComWay2autoJquer.Datepicker.InnerText;
			    Report.Log(ReportLevel.Always,"Date value from text box after changing format: " + dateAfterChangingFormat);
				
			    System.DateTime d1 = System.DateTime.Parse(dateBeforeChangingFormat);
			    System.DateTime d2 = System.DateTime.Parse(dateAfterChangingFormat);
			    
			    Report.Log(ReportLevel.Always,"Original date: " + d1.ToString());
				Report.Log(ReportLevel.Always,"Formatted date: " + d2.ToString());
				
				if(d1 != d2) throw new Exception("After parsing both values from the text box to datetime format, it seems that the datetimes are not equal!");
				else Report.Log(ReportLevel.Always,"The original date and the formatted one are equal after converting to System.DateTime format!");
			} 
			catch (Exception ex) 
			{
				throw ex;
			}
			finally				
			{
				CloseBrowser(processId);
			}
		}
		#endregion
	}
}
