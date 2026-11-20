
Feature: Help Menu Verification

  Scenario: Verify HelpMenu in Demo Mode for Arwen
    Given I launch the app
    And I press 'I dont have a hearing aids' on 'WelcomePage'
    And I press 'Try Demo Mode' on 'DontHaveHIsPage'
    When I press 'Help' on 'MainTabBarPage'
    Then verify 'TutorialsHeader' is 'displayed' on 'HelpMenuPage'
    And verify 'SeeAllTutorials' is 'displayed' on 'HelpMenuPage'


  Scenario: Verify Care and Maintenance and Comfort and Fit Tutorials
    Given I launch the app
    And I press 'I dont have a hearing aids' on 'WelcomePage'
    And I press 'Try Demo Mode' on 'DontHaveHIsPage'
    And I press 'Help' on 'MainTabBarPage'
    When I press 'SeeAllTutorials' on 'HelpMenuPage'
    And I scroll 'Down' and press 'Care and maintenance' on 'TutorialsMenuPage'
    Then verify 'Care and maintenance' is 'displayed' on 'CareAndMaintenancePage'
    When I press Android Back
    And I scroll 'Down' and press 'Comfort and fit' on 'TutorialsMenuPage'
    Then verify 'Comfort and fit' is 'displayed' on 'ComfortAndFitPage'



    Scenario Outline: Verify Adjust HI Sound menu flow for Arwen
    Given I launch the app
    And I press 'I dont have a hearing aids' on 'WelcomePage'
    And I press 'Try Demo Mode' on 'DontHaveHIsPage'
    And I press '<Program>' on 'MainPage'
    When I scroll 'Down' and press 'Adjust your sound' on 'MainPage'
    Then verify 'NoiseReduction' is 'displayed' on 'AdjustHISoundMenuPage'
    And verify 'WindNoiseReduction' is 'displayed' on 'AdjustHISoundMenuPage'
    And verify 'SpeechFocus' is 'displayed' on 'AdjustHISoundMenuPage'
    And verify 'Equalizer' is 'displayed' on 'AdjustHISoundMenuPage'
Examples:
    | Program   |
    | AllAround |
    | BeastMode |
    | Outdoor   |
    | Music     |


   
Scenario Outline: Verify Noise Reduction flow for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press '<ProgramSelector>' on 'MainPage'
And I scroll 'Down' and press 'Adjust your sound' on 'MainPage'
When I press 'NoiseReduction' on 'AdjustHISoundMenuPage'
Then verify 'Strong' is 'displayed' on 'NoiseReductionPage'
And verify 'Considerable' is 'displayed' on 'NoiseReductionPage'
And verify 'Moderate' is 'displayed' on 'NoiseReductionPage'
And verify 'Mild' is 'displayed' on 'NoiseReductionPage'
And verify 'No reduction' is 'displayed' on 'NoiseReductionPage'
When I scroll 'Down' and press '<Selection>' on 'NoiseReductionPage'
Then verify '<Selection>' is 'checked' on 'NoiseReductionPage'
Examples:
| ProgramSelector | Selection     |
| AllAround       | Considerable  |
| BeastMode       | Strong        |
| Outdoor         | No reduction  |
| Music           | Moderate      |
| AllAround       | Mild          |


Scenario: Verify wind noise reduction flow for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press 'Outdoor' on 'MainPage'
And I scroll 'Down' and press 'Adjust your sound' on 'MainPage'
When I press 'WindNoiseReduction' on 'AdjustHISoundMenuPage'
Then verify 'Strong' is 'displayed' on 'WindNoiseReductionPage'
And verify 'Medium' is 'displayed' on 'WindNoiseReductionPage'
And verify 'Mild' is 'displayed' on 'WindNoiseReductionPage'
And verify 'No reduction' is 'displayed' on 'WindNoiseReductionPage'

Scenario Outline: Verify Speech Focus in Demo Mode for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press '<ProgramSelector>' on 'MainPage'
And I scroll 'Down' and press 'Adjust your sound' on 'MainPage'
When I press 'SpeechFocus' on 'AdjustHISoundMenuPage'
Then verify 'Automatic' is 'displayed' on 'SpeechFocusPage'
Then verify 'Narrow' is 'displayed' on 'SpeechFocusPage'
Then verify 'Medium' is 'displayed' on 'SpeechFocusPage'
And I scroll 'Down' and verify 'Wide' is 'displayed' on 'SpeechFocusPage'
When I scroll 'Down' and press '<Selection>' on 'SpeechFocusPage'
Then verify '<Selection>' is 'checked' on 'SpeechFocusPage'
Examples: 
| ProgramSelector | Selection |
| AllAround       | Automatic |
| BeastMode       | Narrow    |
| Outdoor         | Medium    |
| Music           | Wide      |

Scenario: Verify more menu page for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
When I press 'More' on 'MainTabBarPage'
Then verify 'Permissions' is 'displayed' on 'MoreMenuPage'
Then verify 'App analytics' is 'displayed' on 'MoreMenuPage'
Then verify 'Legal' is 'displayed' on 'MoreMenuPage'
Then verify 'Support' is 'displayed' on 'MoreMenuPage'

Scenario: Verify legal menu flow for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
When I press 'More' on 'MainTabBarPage'
And I press 'Legal' on 'MoreMenuPage'
Then verify 'Privacy Policy' is 'displayed' on 'LegalMenuPage'
And verify 'TermsAndConditions' is 'displayed' on 'LegalMenuPage'
When I press 'Privacy Policy' on 'LegalMenuPage'
Then verify 'Privacy policy' is 'displayed' on 'ReadPrivacyPolicyPage'

Scenario: Verify HI settings for Arwen

    Given I launch the app
    And I press 'I dont have a hearing aids' on 'WelcomePage'
    And I press 'Try Demo Mode' on 'DontHaveHIsPage'
    And I press 'Device' on 'MainTabBarPage'
    When I press 'Settings' on 'DeviceMenuPage'
    Then verify 'Settings' is 'displayed' on 'HISettingsPage'
    And verify 'Name' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAid_Model' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAidModelName' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAid_SerialNumber' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAid_SerialNumberText' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAid_FirmwareVersion' is 'displayed' on 'HISettingsPage'
    And verify 'LeftHearingAid_FWVersion' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_Model' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_ModelName' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_SerialNumber' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_SerialNumberText' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_FirmwareVersion' is 'displayed' on 'HISettingsPage'
    And  verify 'RightHearingAid_FWVersion' is 'displayed' on 'HISettingsPage'
    
Scenario: Verify Support page for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press 'More' on 'MainTabBarPage'
When I press 'Support' on 'MoreMenuPage'
Then verify 'HelpSectionInAppBody' is 'displayed' on 'AboutPluginPage'
And verify 'HearingAidCompatibilityLink' is 'displayed' on 'AboutPluginPage'
And verify 'UserGuidesLink' is 'displayed' on 'AboutPluginPage'
And verify 'HelpSectionInAppHeader' is 'displayed' on 'AboutPluginPage'

Scenario: Verify App permission menu for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
When I press 'More' on 'MainTabBarPage'
And I press 'Permissions' on 'MoreMenuPage'
Then verify 'Header' is 'displayed' on 'AppPermissionsPage'
And verify 'BluetoothToggle' is 'displayed' on 'AppPermissionsPage'
And verify 'NotificationToggle' is 'displayed' on 'AppPermissionsPage'

Scenario: Verify startup and Welcome page for Arwen
Given I launch the app
Then verify 'PreparingBody' is 'displayed' on 'StartupPage'
And verify 'WelcomeMessage' is 'displayed' on 'WelcomePage'
And verify 'Connect hearing aids' is 'displayed' on 'WelcomePage'
And verify 'I dont have a hearing aids' is 'displayed' on 'WelcomePage'
When I press 'I dont have a hearing aids' on 'WelcomePage'
Then verify 'DontHaveHiHeader1' is 'displayed' on 'DontHaveHIsPage'
And verify 'DontHaveHiBody1' is 'displayed' on 'DontHaveHIsPage'
And verify 'DontHaveHiHeader2' is 'displayed' on 'DontHaveHIsPage'
And verify 'DontHaveHiBody2' is 'displayed' on 'DontHaveHIsPage'
And verify 'DontHaveHiHeader3' is 'displayed' on 'DontHaveHIsPage'
And verify 'DontHaveHiBody3' is 'displayed' on 'DontHaveHIsPage'
And verify 'Visit our website' is 'displayed' on 'DontHaveHIsPage'
And verify 'Try Demo Mode' is 'displayed' on 'DontHaveHIsPage'

Scenario: Verify Tinnitus Sound Settings for Arwen
    Given I launch the app
    And I press 'I dont have a hearing aids' on 'WelcomePage'
    And I press 'Try Demo Mode' on 'DontHaveHIsPage'
    When I scroll 'Down' and press 'TinnitusSoundSettings' on 'MainPage'
    Then verify 'Header' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'Body' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'Strong' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'Medium' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'No variation' is 'displayed' on 'TinnitusSoundsPage'
    And I scroll 'Down' and verify 'Ocean' is 'displayed' on 'TinnitusSoundsPage'
    And  verify 'CalmingWaves' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'BreakingWaves' is 'displayed' on 'TinnitusSoundsPage'
    And verify 'ShoreLine' is 'displayed' on 'TinnitusSoundsPage'
    And  verify 'BeachSurf' is 'displayed' on 'TinnitusSoundsPage'
    And  verify 'WaterCreek' is 'displayed' on 'TinnitusSoundsPage'

Scenario: Verify Consent for App analytics for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press 'More' on 'MainTabBarPage'
When I press 'App analytics' on 'MoreMenuPage'
Then verify 'Header' is 'displayed' on 'AcceptDataConsentPage'
And verify 'NecessaryDataHeader' is 'displayed' on 'AcceptDataConsentPage'
And verify 'NecessaryDataBody' is 'displayed' on 'AcceptDataConsentPage'
And verify 'StatisticsHeader' is 'displayed' on 'AcceptDataConsentPage'
And verify 'StatisticsBody' is 'displayed' on 'AcceptDataConsentPage'
And verify 'RequiredConsentCheckboxHeader' is 'displayed' on 'AcceptDataConsentPage'
And verify 'RequiredConsentCheckboxText' is 'displayed' on 'AcceptDataConsentPage'
And verify 'OptionalConsentCheckboxHeader' is 'displayed' on 'AcceptDataConsentPage'
And verify 'OptionalConsentCheckboxText' is 'displayed' on 'AcceptDataConsentPage'
And verify 'ReadPrivacyPolicyLink1' is 'displayed' on 'AcceptDataConsentPage'
And verify 'ReadPrivacyPolicyLink2' is 'displayed' on 'AcceptDataConsentPage'
And verify 'Back' is 'displayed' on 'AcceptDataConsentPage'

Scenario: Verify Device Menu for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
When I press 'Device' on 'MainTabBarPage'
Then verify 'HIDeviceName' is 'displayed' on 'DeviceMenuPage'
And verify 'LeftHIBatteryStatus' is 'displayed' on 'DeviceMenuPage'
And verify 'RightHIBatteryStatus' is 'displayed' on 'DeviceMenuPage'
And verify 'ConnectionStatus' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'HI Settings' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'DeviceNameTVStreamerPlus' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'TVStreamerPlusModel' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'TVStreamerPlusImage' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'TVPlusSettings' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'DeviceNameMultiMicPlus' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'MultiMicPlusModel' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'MultiMicPlusImage' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'MMPlusSettings' is 'displayed' on 'DeviceMenuPage'
And I scroll 'Down' and verify 'Pair new accessory' is 'displayed' on 'DeviceMenuPage'

Scenario: Verify Accessory settings for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press 'Device' on 'MainTabBarPage'
When I scroll 'Down' and press 'Pair new accessory' on 'DeviceMenuPage'
Then verify 'Header' is 'displayed' on 'AccessorySelectionPage'
And verify 'back' is 'displayed' on 'AccessorySelectionPage'
And verify 'Multi-Mic+Header' is 'displayed' on 'AccessorySelectionPage'
And verify 'Multi-Mic+' is 'displayed' on 'AccessorySelectionPage'
And verify 'MMBody' is 'displayed' on 'AccessorySelectionPage'
And I scroll 'Down' and verify 'TV-Streamer+Header' is 'displayed' on 'AccessorySelectionPage'
And I scroll 'Down' and verify 'TV-Streamer+' is 'displayed' on 'AccessorySelectionPage'
And I scroll 'Down' and verify 'TVBody' is 'displayed' on 'AccessorySelectionPage'
When I scroll 'Down' and press 'Pair TV-Streamer+' on 'AccessorySelectionPage'
Then verify 'Body' is 'displayed' on 'TurnOnTVSPage'
And verify 'Close' is 'displayed' on 'TurnOnTVSPage'
And verify 'Continue' is 'displayed' on 'TurnOnTVSPage'
When I press 'Continue' on 'TurnOnTVSPage'
Then verify 'Body' is 'displayed' on 'EnterPairingModeTVSPage'
And verify 'SearchForAccessory' is 'displayed' on 'EnterPairingModeTVSPage'
When I press 'SearchForAccessory' on 'EnterPairingModeTVSPage'
Then verify 'Header' is 'displayed' on 'FoundTVSPage'
And verify 'PairWithApp' is 'displayed' on 'FoundTVSPage'
When I press 'PairWithApp' on 'FoundTVSPage'
Then verify 'Body' is 'displayed' on 'PairingCompletedTVSPage'
And verify 'Rename accessory' is 'displayed' on 'PairingCompletedTVSPage'
And verify 'Done' is 'displayed' on 'PairingCompletedTVSPage'
When I press 'Done' on 'PairingCompletedTVSPage'
Then I scroll 'Down' and verify 'TVPlusSettings' is 'displayed' on 'DeviceMenuPage'
When I press 'TVPlusSettings' on 'DeviceMenuPage'
Then verify 'TVStreamerName' is 'displayed' on 'SettingsPage'
And verify 'TVStreamerType' is 'displayed' on 'SettingsPage'
And verify 'TVStreamerSerialNumber' is 'displayed' on 'SettingsPage'
And verify 'StreamAutomatically' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Stereo' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Sounddelay' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Short' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Long' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'PasswordProtectedBroadcast' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'PasswordProtectedBroadcastBody' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'ShowPassword' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Change password' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Forget' is 'displayed' on 'SettingsPage'
When I press 'Close' on 'SettingsPage'
Then I scroll 'Down' and verify 'Pair new accessory' is 'displayed' on 'DeviceMenuPage'
When I scroll 'Down' and press 'Pair new accessory' on 'DeviceMenuPage'
And I scroll 'Down' and press 'Multi-Mic+' on 'AccessorySelectionPage'
Then verify 'Body' is 'displayed' on 'TurnOnMMPage'
And verify 'Close' is 'displayed' on 'TurnOnMMPage'
And verify 'Continue' is 'displayed' on 'TurnOnMMPage'
When I press 'Continue' on 'TurnOnMMPage'
Then verify 'Body' is 'displayed' on 'EnterPairingModeMMPage'
And verify 'SearchForAccessory' is 'displayed' on 'EnterPairingModeMMPage'
When I press 'SearchForAccessory' on 'EnterPairingModeMMPage'
Then verify 'Header' is 'displayed' on 'FoundMMPage'
And verify 'PairWithApp' is 'displayed' on 'FoundMMPage'
When I press 'PairWithApp' on 'FoundMMPage'
Then verify 'Body' is 'displayed' on 'PairingCompletedMMPage'
And verify 'Rename accessory' is 'displayed' on 'PairingCompletedMMPage'
And verify 'Done' is 'displayed' on 'PairingCompletedMMPage'
When I press 'Done' on 'PairingCompletedMMPage'
Then I scroll 'Down' and verify 'MMPlusSettings' is 'displayed' on 'DeviceMenuPage'
When I press 'MMPlusSettings' on 'DeviceMenuPage'
Then verify 'Header' is 'displayed' on 'SettingsPage'
And verify 'MultiMicName' is 'displayed' on 'SettingsPage'
And verify 'MultiMicType' is 'displayed' on 'SettingsPage'
And verify 'MultiMicSerialNumber' is 'displayed' on 'SettingsPage'
And verify 'StreamAutomatically' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'PasswordProtectedBroadcast' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'PasswordProtectedBroadcastBody' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'ShowPassword' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Change password' is 'displayed' on 'SettingsPage'
And I scroll 'Down' and verify 'Forget' is 'displayed' on 'SettingsPage'

Scenario: Verify HI Controls for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
When I press 'Try Demo Mode' on 'DontHaveHIsPage'
Then verify 'AllAround' is 'displayed' on 'MainPage'
And verify 'AllAroundProgramHeader' is 'displayed' on 'MainPage'
And verify 'AllAroundProgramBody' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'TinnitusSoundSettings' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'HearingAidMuteVolume' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'HearingAidCombinedVolumeSlider' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'HearingAidSplitVolume' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'Adjust your sound' is 'displayed' on 'MainPage'
When I scroll 'Up' and press 'BeastMode' on 'MainPage'
Then verify 'BeastMode' is 'displayed' on 'MainPage'
And verify 'HearInNoiseProgramHeader' is 'displayed' on 'MainPage'
And verify 'HearInNoiseProgramBody' is 'displayed' on 'MainPage'
When I press 'Outdoor' on 'MainPage'
Then verify 'Outdoor' is 'displayed' on 'MainPage'
And verify 'OutdoorProgramHeader' is 'displayed' on 'MainPage'
And verify 'OutdoorProgramBody' is 'displayed' on 'MainPage'
When I press 'Music' on 'MainPage'
Then verify 'Music' is 'displayed' on 'MainPage'
And verify 'MusicProgramHeader' is 'displayed' on 'MainPage'
And verify 'MusicProgramBody' is 'displayed' on 'MainPage'
When I press 'AllAround' on 'MainPage'
Then verify 'ReduceNoise' is 'displayed' on 'MainPage'
And verify 'ClarifySpeech' is 'displayed' on 'MainPage'
When I swipe 'Left' on 'ProgramBar' and press 'HearInNoise' in 'MainPage'
Then verify 'ReduceNoise' is 'displayed' on 'MainPage'
And verify 'ClarifySpeech' is 'displayed' on 'MainPage'
When I swipe 'Left' on 'ProgramBar' and press 'Outdoor' in 'MainPage'
Then verify 'ReduceNoise' is 'displayed' on 'MainPage'
And verify 'ClarifySpeech' is 'displayed' on 'MainPage'
When I swipe 'Left' on 'ProgramBar' and press 'Music' in 'MainPage'
Then verify 'IncreaseFullness' is 'displayed' on 'MainPage'
And verify 'IncreaseClarity' is 'displayed' on 'MainPage'
When I swipe 'Left' on 'ProgramBar' and press 'TVAndAudio' in 'MainPage'
Then verify 'Connect a stream' is 'displayed' on 'MainPage'
And I scroll 'Down' and verify 'Read our streaming guide' is 'displayed' on 'MainPage'

Scenario: Verify streaming and auracast flow for arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I swipe 'Left' on 'ProgramBar' and press 'TVAndAudio' in 'MainPage'
When I press 'Connect a stream' on 'MainPage'
Then verify 'AccessoryDevice1' is 'displayed' on 'StreamingPage'
And verify 'BondedStreamer1Body' is 'displayed' on 'StreamingPage'
And verify 'Auracast Broadcast' is 'displayed' on 'StreamingPage'
And verify 'Search for Auracast' is 'displayed' on 'StreamingPage'
When I press 'Search for Auracast' on 'StreamingPage'
Then verify 'PublicAuracast' is 'displayed' on 'StreamingPage'
When I press 'PublicAuracast' on 'StreamingPage'
Then verify 'StopAuracastStreaming' is 'displayed' on 'StreamingPage'
When I press 'StopAuracastStreaming' on 'StreamingPage'
Then verify 'StopAuracastStreaming' is 'Not displayed' on 'StreamingPage'

Scenario: Verify Equalizer flow in Demo Mode for Arwen
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I scroll 'Down' and press 'Adjust your sound' on 'MainPage'
And I press 'Equalizer' on 'AdjustHISoundMenuPage'
And I press 'Adjust now' on 'AdjustEqualizerModalPage' if exists
When I set 'Bass' as '-6' on 'EqualizerPresetPage'
Then verify 'Bass' as '-6' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '-5' on 'EqualizerPresetPage'
Then verify 'Bass' as '-5' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '-4' on 'EqualizerPresetPage'
Then verify 'Bass' as '-4' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '-3' on 'EqualizerPresetPage'
Then verify 'Bass' as '-3' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '-2' on 'EqualizerPresetPage'
Then verify 'Bass' as '-2' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '-1' on 'EqualizerPresetPage'
Then verify 'Bass' as '-1' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '0' on 'EqualizerPresetPage'
Then verify 'Bass' as '0' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '1' on 'EqualizerPresetPage'
Then verify 'Bass' as '1' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '2' on 'EqualizerPresetPage'
Then verify 'Bass' as '2' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '3' on 'EqualizerPresetPage'
Then verify 'Bass' as '3' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '4' on 'EqualizerPresetPage'
Then verify 'Bass' as '4' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '5' on 'EqualizerPresetPage'
Then verify 'Bass' as '5' is 'displayed' on 'EqualizerPresetPage'
When I set 'Bass' as '6' on 'EqualizerPresetPage'
Then verify 'Bass' as '6' is 'displayed' on 'EqualizerPresetPage'

Scenario: Verify volume slider in Demo Hearing aids
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
When I set 'Volume' as '+4' on 'MainPage'
Then verify 'Volume' as '+4' is 'displayed' on 'MainPage'
When I set 'Volume' as '+3' on 'MainPage'
Then verify 'Volume' as '+3' is 'displayed' on 'MainPage'
When I set 'Volume' as '+2' on 'MainPage'
Then verify 'Volume' as '+2' is 'displayed' on 'MainPage'
When I set 'Volume' as '+1' on 'MainPage'
Then verify 'Volume' as '+1' is 'displayed' on 'MainPage'
When I set 'Volume' as '0' on 'MainPage'
Then verify 'Volume' as '0' is 'displayed' on 'MainPage'
When I set 'Volume' as '-1' on 'MainPage'
Then verify 'Volume' as '-1' is 'displayed' on 'MainPage'
When I set 'Volume' as '-2' on 'MainPage'
Then verify 'Volume' as '-2' is 'displayed' on 'MainPage'
When I set 'Volume' as '-3' on 'MainPage'
Then verify 'Volume' as '-3' is 'displayed' on 'MainPage'
When I set 'Volume' as '-4' on 'MainPage'
Then verify 'Volume' as '-4' is 'displayed' on 'MainPage'
When I set 'Volume' as '-9' on 'MainPage'
Then verify 'Volume' as '-9' is 'displayed' on 'MainPage'

Scenario: Verify split volume slider in Demo Hearing aids
Given I launch the app
And I press 'I dont have a hearing aids' on 'WelcomePage'
And I press 'Try Demo Mode' on 'DontHaveHIsPage'
And I press 'split' on 'MainPage'
When I set 'Leftvolume' as '+4' on 'MainPage'
Then verify 'Leftvolume' as '+4' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '+3' on 'MainPage'
Then verify 'Leftvolume' as '+3' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '+2' on 'MainPage'
Then verify 'Leftvolume' as '+2' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '+1' on 'MainPage'
Then verify 'Leftvolume' as '+1' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '0' on 'MainPage'
Then verify 'Leftvolume' as '0' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '-1' on 'MainPage'
Then verify 'Leftvolume' as '-1' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '-2' on 'MainPage'
Then verify 'Leftvolume' as '-2' is 'displayed' on 'MainPage'
When I set 'Leftvolume' as '-9' on 'MainPage'
Then verify 'Leftvolume' as '-9' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '+4' on 'MainPage'
Then verify 'Rightvolume' as '+4' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '+3' on 'MainPage'
Then verify 'Rightvolume' as '+3' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '+2' on 'MainPage'
Then verify 'Rightvolume' as '+2' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '+1' on 'MainPage'
Then verify 'Rightvolume' as '+1' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '0' on 'MainPage'
Then verify 'Rightvolume' as '0' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '-1' on 'MainPage'
Then verify 'Rightvolume' as '-1' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '-2' on 'MainPage'
Then verify 'Rightvolume' as '-2' is 'displayed' on 'MainPage'
When I set 'Rightvolume' as '-9' on 'MainPage'
Then verify 'Rightvolume' as '-9' is 'displayed' on 'MainPage'



