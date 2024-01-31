using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public PhotonView playerSetupView;

    private int selectedWeapon = 0;

    public Animation _animation;
    public AnimationClip draw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon += 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon -= 1;
            }
        }
        // Nếu vũ khí đã chuyển đổi, gọi hàm SelectWeapon
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        // Sử dụng Remote Procedure Call (RPC) để đặt vũ khí được chọn cho tất cả người chơi
        playerSetupView.RPC("SetTPWeapon", RpcTarget.All, selectedWeapon);
        // Đảm bảo rằng selectedWeapon không vượt quá số lượng vũ khí có sẵn
        if (selectedWeapon >= transform.childCount)
        {
            selectedWeapon = transform.childCount - 1;
        }

        _animation.Stop();
        _animation.Play(draw.name);

        // Duyệt và hiển thị vũ khí được chọn, ẩn các vũ khí khác
        int i = 0;
        foreach (Transform _weapon in transform)
        {
            if(i == selectedWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
