using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour {
    public float speed;
    public GameObject player;
    //public GameObject healthBar;
    public GameObject ammoBar;
    public GameObject gasBar;
    public Text passengersText;
	public Text earningsText;

    private Rigidbody2D rigidBody;
    private ArrayList passengers;
    private bool pickingUp;
    private bool droppingOff;
    private bool droppingOff1;
    private bool droppingOff2;
    private bool droppingOff3;
    private bool droppingOff4;
    private float earnings;
    private float rating;
    private float totalStars;

    private GameObject goBullet;
    private float dropOffCount;
    private float maxHealth;
    private float maxAmmo;
    private float maxGas;
    private float health;
    private float ammo;
    private float gas;
    private float ammoVal;
    private float gasVal;
    
    
    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        dropOffCount = 0f;
        earnings = 0f;
        totalStars = 0f;
		rating = 0f;
        passengers = new ArrayList();

        maxAmmo = 20f; // Only 20 bullets available
        maxGas = 20f; //20/20 is full
        ammo = 10f;
        gas = 20f;
        ammoVal = (ammo / maxAmmo) * 1.95f; //1.95f is the width of all the bars
        gasVal = (gas / maxGas) * 1.95f;
		setBars(/*healthVal, */ammoVal, gasVal);
        setPassengersText();
		setEarningsText ();
    }

	private void setEarningsText() {
		String text = "Earnings: $" + earnings.ToString();
		earningsText.text = text;
	}

    private void setPassengersText() {
        String text = "Passengers: ";
        for (int i = 0; i < passengers.Count; i++ ) {
            text = text + passengers[i] + " " ;
        }
        passengersText.text = text;
    }
	
    private void setBars(/*float healthVal,*/ float ammoVal, float gasVal) {
        //healthBar.transform.localScale = new Vector3 (healthVal, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        ammoBar.transform.localScale = new Vector3 (ammoVal, ammoBar.transform.localScale.y, ammoBar.transform.localScale.z);
        gasBar.transform.localScale = new Vector3 (gasVal, gasBar.transform.localScale.y, gasBar.transform.localScale.z);
    }

	public float returnGas() {
		return gas;
	}

	public float returnHealth() {
		return health;
	}

	public float returnRating() {
		return rating;
	}

	public float returnEarnings() {
		return earnings;
	}

    void FixedUpdate () {
        float xV = 0f;
        float yV = 0f;
        float zV = 0f;
        bool up = Input.GetKey ("w") || Input.GetKey ("up");
        bool down = Input.GetKey ("s") || Input.GetKey ("down");
        bool left = Input.GetKey ("a") || Input.GetKey ("left");
        bool right = Input.GetKey ("d") || Input.GetKey ("right");
        bool pickUp = Input.GetKey("q");
        bool dropOff = Input.GetKey("e");
        bool un = Input.GetKey("1");
        bool deux = Input.GetKey("2");
        bool trois = Input.GetKey("3");
        bool quatre = Input.GetKey("4");
        bool shoot = Input.GetButtonDown("Shoot");

		if (up && right) {
			yV = speed;
			xV = speed;
			transform.rotation = Quaternion.AngleAxis (45, Vector3.back);
		} else if (up && left) {
			yV = speed;
			xV = -speed;
			transform.rotation = Quaternion.AngleAxis (45, Vector3.forward);
		} else if (down && left) {
			yV = -speed;
			xV = -speed;
			transform.rotation = Quaternion.AngleAxis (135, Vector3.forward);
		} else if (down && right) {
			yV = -speed;
			xV = speed;
			transform.rotation = Quaternion.AngleAxis (135, Vector3.back);
		} else if (up) {
			yV = speed;
			transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
		} else if (down) {
			yV = -speed;
			transform.rotation = Quaternion.AngleAxis (180, Vector3.back);
		} else if (left) {
			xV = -speed;
			transform.rotation = Quaternion.AngleAxis (90, Vector3.forward);
		} else if (right) {
			xV = speed;
			transform.rotation = Quaternion.AngleAxis (90, Vector3.back);
        } else if (pickUp) {
            pickingUp = true;
            droppingOff = false;
        } else if (un) {
            droppingOff = true;
            droppingOff1 = true;
            pickingUp = false;
        } else if (deux) {
            droppingOff = true;
            droppingOff2 = true;
            pickingUp = false;
        } else if (trois) {
            droppingOff = true;
            droppingOff3 = true;
            pickingUp = false;
        } else if (quatre) {
            droppingOff = true;
            droppingOff4 = true;
            pickingUp = false;
        }
        rigidBody.velocity = new Vector3 (xV, yV, zV);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        String triggeredObject = other.gameObject.tag;
        switch (triggeredObject) {
        case ("Ammo"):
            other.gameObject.SetActive (false);
            if (ammo < 19f) {// I used 19 so 18 can be the max boundary
                ammo = ammo + 2.0f;
                ammoVal = (ammo / maxAmmo) * 1.95f;
                setBars (/*healthVal, */ammoVal, gasVal);
            }
            break;
        case ("Gas"):
            other.gameObject.SetActive (false);
            if (gas < 19f) {// I used 19 so 18 can be the max boundary
                gas = gas + 2.0f;
                gasVal = (gas / maxGas) * 1.95f; //change this to global variable bar length
				setBars (/*healthVal, */ammoVal, gasVal);
            }
            break;
        case ("Rock"):
        case ("Spinner"):
        case ("Spike"):
            if ((health - 1.0f) == 0.0f) {
                health = health - 1.0f;
                gameOver ();
            } else {
                health = health - 1.0f;
				setBars (/*healthVal, */ammoVal, gasVal);
            }
            break;
        default:
            break;
        }
    }
    
    void OnTriggerStay2D(Collider2D other) {
        GameObject passenger;
        String triggeredObject = other.gameObject.tag;
        GameObject dropZone;
        switch (triggeredObject) {
        
		case ("Pick Up"):
			passenger = other.gameObject.transform.GetChild (0).gameObject;
			if (pickingUp && passenger.activeSelf) {
				passenger.SetActive (false);
				passengers.Add (passenger.name);
			}
            pickingUp = false;
            break;
            
		case ("Drop Off"):
			dropZone = other.gameObject;
			string zoneName = dropZone.name;
			if (passengers.IndexOf (zoneName) != -1) {         
				if (droppingOff) {
					if (droppingOff1) {
						dropOffIndex (0, zoneName);
						droppingOff1 = false;
					} else if (droppingOff2) {
						dropOffIndex (1, zoneName);
						droppingOff2 = false;
					} else if (droppingOff3) {
						dropOffIndex (2, zoneName);
						droppingOff3 = false;
					} else if (droppingOff4) {
						dropOffIndex (3, zoneName);
						droppingOff4 = false;
					}
				}
			}
			droppingOff = false;
            break;
        default:
                break;
        }
    }
    
    private void dropOffIndex(int index, String zoneName) {
        string passengerName = (string) passengers[index];
		if (passengerName == zoneName) {
			if (droppingOff) {
				passengers.Remove(passengerName);
				dropOffCount++;
				earnings = earnings + 2;
				setEarningsText();
				receiveRating ();
			}
            droppingOff = false;
        }
    }
        
    private void receiveRating() {
        System.Random rand = new System.Random();
        int difficulty = rand.Next(1, 4);
        float newRating = 0f;
        switch (difficulty) {
            case 1: //Medium diff
                newRating = (float) rand.Next(1, 5);
                break;
            case 2: //Hard diff
                newRating = (float) rand.Next(0, 4);
                break;
            case 3: //Savage Diff
                newRating = (float) rand.Next(0, 2);
                break;
            default:
                break;
        }
        totalStars = totalStars + newRating;
        rating = (float) Math.Round((double) (totalStars / dropOffCount), 2);
    }
    private void gameOver() {
        //Call GAmeover in Game manager
    }
    
    void Update() {
        //setDisplay (passengers, lives, ammo, gas, rating);
        gas = gas - 0.01f;
        if (gas < 0f) {
            gasVal = 0.0f;
            gameOver ();
        } else if (health < 0f) {
            //healthVal = 0.0f;
        } else {
            gasVal = (gas / maxGas) * 1.95f;
        }
        
        //Alert when approaching drop off zone
		setBars (/*healthVal, */ammoVal, gasVal);
        setPassengersText();
    }
}
