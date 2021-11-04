using System.Collections;
using UnityEngine;

public class AkAssultRifle : MonoBehaviour
{
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject MuzzleFlashEffect;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip AudioClipTakeOutWeapon; //무기 동작 사운드
    [SerializeField]
    private AudioClip AudioClipFire;

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //무기 설정

    private float lastAttackTime = 0; //마지막 발사시간 체크

    private AudioSource audioSource; //사운드 컴포넌트
    private PlayerAnimController animator; //공격 애니메이션 재생

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimController>();
    }

    private void OnEnable()
    {
        MuzzleFlashEffect.SetActive(false);
        PlaySound(AudioClipTakeOutWeapon);

    }

    public void StartAttack(int type=0)
    {
        if(type==0)
        {
            if(weaponSetting.isAutoAttack==true)
            {
                StartCoroutine("OnAttackLoop");
            }
            else
            {
                OnAttack();
            }
        }

    }
    public void StopAttack(int type=0)
    {
        if(type==0)
        {
            StopCoroutine("OnAttackLoop");
        }
    }

    private IEnumerator OnAttackLoop()
    {
        while(true)
        {
            OnAttack();

            yield return null;
        }
    }

    public void OnAttack()
    {
        if(Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            if(animator.Movespeed > 0.5f)
            {
                return;
            }

            lastAttackTime = Time.time;

            animator.AnimPlay("Fire", -1, 0); //같은 애니메이션을 반복할때 끊고 처음부터 다시 재생

            StartCoroutine("OnMuzzleFlashEffect");
            PlaySound(AudioClipFire);
            
        }
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        MuzzleFlashEffect.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate * 0.3f);

        MuzzleFlashEffect.SetActive(false);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void WeaponInteraction()
    {
        animator.AnimPlay("Inspect", 0, 0);
    }
}
