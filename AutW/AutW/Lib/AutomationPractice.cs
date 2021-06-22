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
	/// Description of AutomationPractice.
	/// </summary>
	[UserCodeCollection]
	public static class AutomationPractice
	{
		#region Methods
		
		//[UserCodeMethod]
		public static int OpenBrowserAndNavigateToPage()
		{		
			string url = "http://automationpractice.com/index.php";
			Report.Log(ReportLevel.Always,"Open Chrome browser and navigate to page: " + url);
			return Host.Current.OpenBrowser(url, "Chrome", "", false, false, false, false, false, true);		
		}
		
		//[UserCodeMethod]
		public static void CloseBrowser(int processId)
		{
			Report.Log(ReportLevel.Always,"Int retrieved from OpenBrowswer method: " + processId.ToString());
			Report.Log(ReportLevel.Always,"Trying to close Chrome ...");
			//Host.Current.CloseApplication(processId);
			
		}
		
		#endregion
		
		#region TestCases
		[UserCodeMethod]
		public static void TC_Register()
		{
			int processId = OpenBrowserAndNavigateToPage();
			try 
			{
				var repository = AutomationPracticeRepository.Instance.MyStore;
				
				Report.Log(ReportLevel.Always,"Click Sign In...");				
				repository.SignInLink.Click();
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Type an email address in the associated text box...");
				repository.EmailAddressTextBox.PressKeys("dummy10@yahoo.com");
				Report.Log(ReportLevel.Always,"Inserted email address: " + repository.EmailAddressTextBox.TagValue);
				Ranorex.Delay.Seconds(2);
				
				Report.Log(ReportLevel.Always,"Click Create an Account...");	
				repository.CreateAnAccountButton.Click();
				Ranorex.Delay.Seconds(1);
				
				var formFields = repository.FormFields;
				
				//populate fields
				Report.Log(ReportLevel.Always,"Select title...");
				formFields.IdGender.Click();
				formFields.IdGender.Click();
				Ranorex.Delay.Seconds(1);
				
				string firstName = "FirstName";
				Report.Log(ReportLevel.Always,"Update First Name: " + firstName);
				//formFields.CustomerFirstname.InnerText = "FirstName";
				formFields.CustomerFirstname.PressKeys(firstName + "{TAB}");
				Ranorex.Delay.Seconds(1);
				
				string lastName = "LastName";				
				Report.Log(ReportLevel.Always,"Update Last Name: " + lastName);
				//.CustomerLastname.InnerText= "LastName";
				formFields.CustomerLastname.PressKeys(lastName + "{TAB}");
				Ranorex.Delay.Seconds(1);
				
				Report.Log(ReportLevel.Always,"Update Password...");
				//formFields.Passwd.InnerText = "pwd123";
				formFields.Passwd.PressKeys("pwd123{TAB}");
				Ranorex.Delay.Seconds(1);
				
//				Report.Log(ReportLevel.Always,"Update Address First Name...");
//				formFields.AddressFirstname.PressKeys("FirstName");
//				
//				Report.Log(ReportLevel.Always,"Update Address Last Name...");
//				formFields.AddressLastname.PressKeys("LastName");
				
				string address = "101 Street";
				Report.Log(ReportLevel.Always,"Update Address: " + address);
				formFields.Address1.PressKeys(address + "{TAB}");
				Ranorex.Delay.Seconds(1);
				
				string city = "Timisoara";
				Report.Log(ReportLevel.Always,"Update City: " + city);
				formFields.City.PressKeys(city + "{TAB}");
				Ranorex.Delay.Seconds(1);
				
				string state = "New York";
				Report.Log(ReportLevel.Always,"Update State: " + state);
				List<OptionTag> states = formFields.IdState.FindDescendants<OptionTag>().ToList();
				OptionTag newYorkState = states.Where(o=>o.Label == state).FirstOrDefault();
				newYorkState.Select();
				Ranorex.Delay.Seconds(1);
				
				string postalCode = "40020";
				Report.Log(ReportLevel.Always,"Update Postal Code: " + postalCode);
				formFields.Postcode.PressKeys(postalCode + "{TAB}");
				//formFields.Postcode.InnerText = "+40020";
				Ranorex.Delay.Seconds(1);
				
				string mobilePhone = "+40232132191";
				Report.Log(ReportLevel.Always,"Update Mobile Phone: " + mobilePhone);
				//formFields.PhoneMobile.InnerText = "+40232132191";
				formFields.PhoneMobile.PressKeys(mobilePhone + "{TAB}");
				Ranorex.Delay.Seconds(1);
				formFields.Register.Focus();
				
				//Report.Log(ReportLevel.Always,"Update Alias...");
				//formFields.Alias.PressKeys("HomeAddress");
				//Ranorex.Delay.Seconds(1);
				
				Report.Log(ReportLevel.Always,"Register...");
				formFields.Register.Click();
				Ranorex.Delay.Seconds(3);	
				
				Report.Log(ReportLevel.Always,"Go To My Addresses section...");
				repository.MyAddresses.Click();
				Ranorex.Delay.Seconds(1);
				
				string extractedFirstName = repository.LoggedInAddress.SpanTagFirstName.InnerText;
				string extractedLastName = repository.LoggedInAddress.SpanTagLastName.InnerText;
				string extractedAddress = repository.LoggedInAddress.SpanTagAddress.InnerText;
				string extractedCity = repository.LoggedInAddress.SpanTagCity.InnerText;
				string extractedState = repository.LoggedInAddress.SpanTagState.InnerText;
				string extractedPostalCode = repository.LoggedInAddress.SpanTagPostcode.InnerText;
				string extractedCountry = repository.LoggedInAddress.SpanTagCountry.InnerText;
				string extractedMobilePhone = repository.LoggedInAddress.SpanTagMobile.InnerText;				
				
				Report.Log(ReportLevel.Always,"First Name: " + extractedFirstName);
				Report.Log(ReportLevel.Always,"Last Name: " + extractedLastName);
				Report.Log(ReportLevel.Always,"Address: " + extractedAddress);
				Report.Log(ReportLevel.Always,"City: " + extractedCity);
				Report.Log(ReportLevel.Always,"State: " + extractedState);
				Report.Log(ReportLevel.Always,"Postal Code: " + extractedPostalCode);
				Report.Log(ReportLevel.Always,"Country: " + extractedCountry);
				Report.Log(ReportLevel.Always,"Mobile Phone: " + extractedMobilePhone);
				
				string errorMessage = "";
				if(firstName != extractedFirstName.Trim()) errorMessage += "First Name is not the same as the one inserted on register form!" + System.Environment.NewLine;
				if(lastName != extractedLastName.Trim()) errorMessage += "Last Name is not the same as the one inserted on register form!" + System.Environment.NewLine;
				if(address != extractedAddress.Trim()) errorMessage += "Address is not the same as the one inserted on register form!" + System.Environment.NewLine;
				if(!extractedCity.Trim().Contains(city)) errorMessage += "City is not the same as the one inserted on register form!" + System.Environment.NewLine;
				if(state != extractedState.Trim()) errorMessage += "State is not the same as the one inserted on register form!" + System.Environment.NewLine;
				if(postalCode != extractedPostalCode.Trim()) errorMessage += "Postal Code is not the same as the one inserted on register form!" + System.Environment.NewLine;
				//if( != extractedCountry) errorMessage += "First Name is not the same as the one inserted on register form!" + System.Environment.NewLine;
						
				Report.Log(ReportLevel.Always,"Sign out...");
				repository.LogMeOut.Click();
				Ranorex.Delay.Seconds(1);
				
				if(errorMessage.Length > 1) throw new Exception(errorMessage);
				
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
