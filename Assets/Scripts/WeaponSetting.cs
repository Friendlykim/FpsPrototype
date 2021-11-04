[System.Serializable] //무기 종류가 다양할때 공용으로 사용할 변수를 구조체로 묶어 관리한다
public struct WeaponSetting
{
    public float attackRate;
    public float attackRange;
    public bool isAutoAttack;
}