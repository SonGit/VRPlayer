using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private LogoPanel LogoPanel;
    [SerializeField] private MenuPanel MenuPanel;
    [SerializeField] private SignInPanel SignInPanel;
    [SerializeField] private SignUpPanel SignUpPanel;
    [SerializeField] private RecoveryPanel RecoveryPanel;
    [SerializeField] private TermsPanel TermsPanel;
    [SerializeField] private WalkthroughPanel WalkthroughPanel;
    [SerializeField] private ProfilePanel ProfilePanel;
    [SerializeField] private AboutUsPanel AboutUsPanel;
    [SerializeField] private HomePanel HomePanel;
    [SerializeField] private PortfolioPanel PortfolioPanel;
    [SerializeField] private SettingsPanel SettingsPanel;

    private void Start()
    {
        SignUpPanel.OnSignUp += SignUpPanel_OnSignUp;
        SignUpPanel.OnAlreadyAccount += SignUpPanel_OnAlreadyAccount;

        SignInPanel.OnLogin += SignInPanel_OnLogin;
        SignInPanel.OnCreate += SignInPanel_OnCreate;
        SignInPanel.OnForgot += SignInPanel_OnForgot;

        RecoveryPanel.OnSend += RecoveryPanel_OnSend;
        RecoveryPanel.OnAlreadyAccount += RecoveryPanel_OnAlreadyAccount;

        WalkthroughPanel.OnClick += WalkthroughPanel_OnClick;

        MenuPanel.OnHome += MenuPanel_OnHome;
        MenuPanel.OnProfile += MenuPanel_OnProfile;
        MenuPanel.OnAboutUs += MenuPanel_OnAboutUs;
        MenuPanel.OnPortfolio += MenuPanel_OnPortfolio;
        MenuPanel.OnSettings += MenuPanel_OnSettings;
        MenuPanel.OnLogout += MenuPanel_OnLogout;

        ProfilePanel.OnBack += ProfilePanelOnBack;
        AboutUsPanel.OnBack += AboutUsPanelOnBack;
        HomePanel.OnBack += HomePanelOnBack;
        PortfolioPanel.OnBack += PortfolioPanelOnBack;
        SettingsPanel.OnBack += SettingsPanelOnBack;

        Init();
    }

    private void Init()
    {
        WalkthroughPanel.SetActive(true);
    }

    private void WalkthroughPanel_OnClick()
    {
        WalkthroughPanel.SetActive(false);

        LogoPanel.SetActive(true);
        TermsPanel.SetActive(true);
        SignUpPanel.SetActive(true);
    }

    private void SignUpPanel_OnSignUp()
    {
        
    }
    private void SignUpPanel_OnAlreadyAccount()
    {
        SignUpPanel.SetActive(false);

        SignInPanel.SetActive(true);
    }

    private void SignInPanel_OnLogin()
    {
        LogoPanel.SetActive(false);
        TermsPanel.SetActive(false);
        SignInPanel.SetActive(false);

        MenuPanel.SetActive(true);
    }
    private void SignInPanel_OnCreate()
    {
        SignInPanel.SetActive(false);

        SignUpPanel.SetActive(true);
    }
    private void SignInPanel_OnForgot()
    {
        SignInPanel.SetActive(false);

        RecoveryPanel.SetActive(true);
    }

    private void RecoveryPanel_OnSend()
    {
        
    }
    private void RecoveryPanel_OnAlreadyAccount()
    {
        RecoveryPanel.SetActive(false);

        SignInPanel.SetActive(true);
    }

    private void ProfilePanelOnBack()
    {
        ProfilePanel.SetActive(false);

        MenuPanel.SetActive(true);
    }

    private void MenuPanel_OnHome()
    {
        MenuPanel.SetActive(false);

        HomePanel.SetActive(true);
    }
    private void MenuPanel_OnProfile()
    {
        MenuPanel.SetActive(false);

        ProfilePanel.SetActive(true);
    }
    private void MenuPanel_OnAboutUs()
    {
        MenuPanel.SetActive(false);

        AboutUsPanel.SetActive(true);
    }
    private void MenuPanel_OnPortfolio()
    {
        MenuPanel.SetActive(false);

        PortfolioPanel.SetActive(true);
    }
    private void MenuPanel_OnSettings()
    {
        MenuPanel.SetActive(false);

        SettingsPanel.DisableParams();
        SettingsPanel.SetActive(true);
    }

    private void AboutUsPanelOnBack()
    {
        AboutUsPanel.SetActive(false);

        MenuPanel.SetActive(true);
    }

    private void HomePanelOnBack()
    {
        HomePanel.SetActive(false);

        MenuPanel.SetActive(true);
    }

    private void PortfolioPanelOnBack()
    {
        PortfolioPanel.SetActive(false);

        MenuPanel.SetActive(true);
    }

    private void SettingsPanelOnBack()
    {
        SettingsPanel.SetActive(false);

        MenuPanel.SetActive(true);
    }

    private void MenuPanel_OnLogout()
    {
        MenuPanel.SetActive(false);

        WalkthroughPanel.SetActive(true);
    }
}