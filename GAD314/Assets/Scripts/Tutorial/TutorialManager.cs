using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;

    [SerializeField] private PlayerMovement2 movement;
    [SerializeField] private GrapplingGun grappling;
    [SerializeField] private Shooting shooting;
    [SerializeField] private MeleeAttack katana;

    [SerializeField] private TMP_Text checkOneText;
    [SerializeField] private TMP_Text checkTwoText;
    [SerializeField] private TMP_Text checkThreeText;

    [SerializeField] private WeaponManager weapon;

    private bool movementTutorial = false;
    private bool katanaTutorial = false;
    private bool shootTutorial = false;
    private bool grapplingTutorial = false;

    // private bool w,a,s,d,j,sh = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!movementTutorial)
        {
            MovementTutorial();
        }
        if(!katanaTutorial && weapon.katana)
        {
            tutorialPanel.SetActive(true);
            KatanaTutorial();
        }
        if(!shootTutorial && weapon.gun)
        {
            tutorialPanel.SetActive(true);
            ShootTutorial();
        }
        if(!grapplingTutorial && weapon.grapplingGun)
        {
            tutorialPanel.SetActive(true);
            GrapplingTutorial();
        }
        
    }

    public void MovementTutorial()
    {
        tutorialText.text = "Press WASD to move. Press Space to jump. \n Press L Shift to sprint.";
        checkOneText.text = "Move using WASD";
        checkTwoText.text = "Sprint 'Left Shift'";
        checkThreeText.text = "Jump 'Space'";

        if(movement.m)
        {
            checkOneText.color = Color.green;
        }
        if(movement.r)
        {
            checkTwoText.color = Color.green;
        }
        if(movement.j)
        {
            checkThreeText.color = Color.green;
        }

        if(movement.m && movement.r && movement.j)
        {
            movementTutorial = true;
            StartCoroutine(CompletedTutorial());
        }
    }

    public void KatanaTutorial()
    {
        tutorialText.text = "Press 1 to hold and hide the katana. \n Press Left Click to attack with katana.";
        checkOneText.text = "Attack with blade.";

        if(katana.used)
        {
            checkOneText.color = Color.green;
            katanaTutorial = true;
            StartCoroutine(CompletedTutorial());
        }
    }

    public void ShootTutorial()
    {
        tutorialText.text = "Press 2 to hold and hide the gun. \n Press Left Click to shoot.";
        checkOneText.text = "Shoot with gun";

        if(shooting.used)
        {
            checkOneText.color = Color.green;
            shootTutorial = true;
            StartCoroutine(CompletedTutorial());
        }
    }

    public void GrapplingTutorial()
    {
        tutorialText.text = "Press 3 to hold and hide the grappling gun. \n Press Right Click to start grappling.";
        checkOneText.text = "Use grappling gun";

        if(grappling.used)
        {
            checkOneText.color = Color.green;
            grapplingTutorial = true;
            StartCoroutine(CompletedTutorial());
        }
    }

    public IEnumerator CompletedTutorial()
    {
        yield return new WaitForSeconds(2f);
        tutorialPanel.SetActive(false);
        checkOneText.color = Color.white;
        checkTwoText.color = Color.white;
        checkThreeText.color = Color.white;
        checkOneText.text = "";
        checkTwoText.text = "";
        checkThreeText.text = "";
    }
}
