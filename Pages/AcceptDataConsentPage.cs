using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arwendemo.Pages
{
    public class AcceptDataConsentPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "Header",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Paragraph1HeaderText"
                },

                {
                    "NecessaryDataHeader",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Paragraph2SubHeaderText"
                },
                {
                    "NecessaryDataBody",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Paragraph2BodyText"
                },
                {
                    "StatisticsHeader",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Paragraph3SubHeaderText"
                },
                {
                    "StatisticsBody",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Paragraph3BodyText"
                },
                {
                    "RequiredConsentCheckboxHeader",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Checkbox1HeaderText"
                },
                {
                    "RequiredConsentCheckboxText",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.CheckboxText1"
                },
                {
                    "OptionalConsentCheckboxHeader",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Checkbox2HeaderText"
                },
                {
                    "OptionalConsentCheckboxText",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.CheckboxText2"
                },
                {
                    "ReadPrivacyPolicyLink1",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.TertiaryButtonText1"
                },
                {
                    "ReadPrivacyPolicyLink2",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.TertiaryButtonText2"
                },
                {
                    "Save",
                    "ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.PrimaryButtonText"
                },
                {
                    "Back",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ConsentForAppAnalytics.Pages.AcceptDataConsentPage.Back"
                }
            };

        public AcceptDataConsentPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public bool IsElementDisplayed(string elementName)
        {
            string resourceId = GetElementId(elementName);

            var elements = driver.FindElements(By.Id(resourceId));

            Console.WriteLine(
                elementName + " resource ID matches: " + elements.Count
            );

            foreach (var element in elements)
            {
                try
                {
                    Console.WriteLine(
                        "Text: " + element.Text +
                        ", Displayed: " + element.Displayed
                    );

                    if (element.Displayed)
                        return true;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Element became stale: " + elementName);
                }
            }

            return false;
        }

        public string GetElementId(string elementName)
        {
            if (elementIds.TryGetValue(elementName, out string resourceId))
            {
                return resourceId;
            }

            throw new Exception("Unknown element: " + elementName);
        }
    }
}