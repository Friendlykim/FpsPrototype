using System.Collections;
using UnityEngine;

public class AkAssultRifle : MonoBehaviour
{
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject MuzzleFlashEffect;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip AudioClipTakeOutWeapon; //���� ���� ����
    [SerializeField]
    private AudioClip AudioClipFire;

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //���� ����

    private float lastAttackTime = 0; //������ �߻�ð� üũ

    private AudioSource audioSource; //���� ������Ʈ
    private PlayerAnimController animator; //���� �ִϸ��̼� ���

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

            animator.AnimPlay("Fire", -1, 0); //���� �ִϸ��̼��� �ݺ��Ҷ� ���� ó������ �ٽ� ���

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
