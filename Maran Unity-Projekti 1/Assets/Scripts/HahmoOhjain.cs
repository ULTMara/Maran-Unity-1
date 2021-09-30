using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HahmoOhjain : MonoBehaviour
{
    public float juoksuNopeus = 3.0f;
    public float hiireNopeus = 3.0f;
    public float maxKaannosAsteet = 60;
    public float minKaannosAsteet = -70;
    public float hyppyNopeus = 10f;
    public float painovoima = 10;
    public CursorLockMode haluttuMoodi;
    private float vertikaalinenPyorinta = 0;
    private float horisontaalinenPyorinta = 0;
    private Vector3 liikesuunta = Vector3.zero;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = haluttuMoodi;
        Cursor.visible = (CursorLockMode.Locked != haluttuMoodi);

    }

    // Update is called once per frame
    void Update()
    {
        horisontaalinenPyorinta += Input.GetAxis("Mouse X") * hiireNopeus;
        vertikaalinenPyorinta -= Input.GetAxis("Mouse Y") * hiireNopeus;
        vertikaalinenPyorinta = Mathf.Clamp(vertikaalinenPyorinta, minKaannosAsteet, maxKaannosAsteet);
        Camera.main.transform.localRotation = Quaternion.Euler(vertikaalinenPyorinta,horisontaalinenPyorinta, 0);

        float nopeusEteen = Input.GetAxis("Vertical");
        float nopeusSivulle = Input.GetAxis("Horizontal");

        Vector3 nopeus = new Vector3(nopeusSivulle, 0, nopeusEteen);
        nopeus = transform.rotation * nopeus;

        controller.SimpleMove(nopeus * juoksuNopeus);

        liikesuunta.y = liikesuunta.y - painovoima * Time.deltaTime;

        controller.Move(liikesuunta * Time.deltaTime);

        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            liikesuunta.y += hyppyNopeus;
        }
            
    }
}
