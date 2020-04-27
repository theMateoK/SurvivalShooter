using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float brzina = 6f; //Brzina kojom će se naš igrač kretati
    Vector3 kretanje; //Vektor za spremanje pokreta igrača
    Animator anim; //Referenca na komponentu
    Rigidbody igracevRigidbody; //Referenca na igračev Rididbody
    int podnaMaska; //Maska poda može biti provedena s gameobjects na podu
    float camRayLength = 100f; //Duljina zrake kamere na sceni
    void Awake()
    {
        //Napravimo slojnu masku za pod
        podnaMaska = LayerMask.GetMask("Floor");
        //Postavljanje referenci
        anim = GetComponent<Animator>();
        igracevRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //Spremanje ulaznih osi
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Edit-->Project Settings-->Input --->Horizontal... Vertical...
        //Micanje igrača po sceni
        Move(h, v);
        Turning();
        Animating(h, v);
        //Okretanje lica igrača u smjeru pokazivača miša
        //Animiranje igrača
    }
    void Move(float h, float v)
    {
        //postavljanje vektora kretanja na temelju ulazne vrijednosti
        kretanje.Set(h, 0f, v);
        //Vektor kretanja treba biti proporcionalan kretanju i vremenu
        kretanje = kretanje.normalized * brzina * Time.deltaTime;
       
    //Premještanje igrača na trenutni položaj nakon pokreta
 igracevRigidbody.MovePosition(transform.position + kretanje);
    }
    void Turning()
    {
        //Izrada zrake od pokazivača miša na sceni iz smjera kamere
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Izrada varijable za pohranu podataka podPogodak
        RaycastHit podPogodak;
        //Izvrši mi Raycast i ako pogodi nešto na podnaMaska
        if (Physics.Raycast(camRay, out podPogodak, camRayLength, podnaMaska))
        {
            //Izrada vektora od igrača do točke na podu Raycast od miša pucaj
            Vector3 odIgracaDoMisa = podPogodak.point - transform.position;
            //Osigurati da je vektor u cijelosti duž poda
            odIgracaDoMisa.y = 0f;
            //Kreiranje rotacije na temelju vektora od igrača do pokazivača miša
            Quaternion novaRotacija = Quaternion.LookRotation(odIgracaDoMisa);
            //Igrač se rotira na temelju nove rotacije
            igracevRigidbody.MoveRotation(novaRotacija);
        }
    }
    void Animating(float h, float v)
    {
        //Napravimo boolean tip, ako je istina da ni jedna od ulaznih osi nije nula
        bool hoda = h != 0f || v != 0f;
        //Trebamo obavjestiti animatora je li igrač hoda ili ne
        anim.SetBool("DaHoda", hoda);
    }

}
