using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
    {
    public GameObject _P_PrefabGameObject;
    public int _P_PooledAmount;

    [HideInInspector] public List<GameObject> _P_Objects;

    void Awake() {
        for (int i = 0; i < _P_PooledAmount; i++) {
            GameObject obj = (GameObject)Instantiate(_P_PrefabGameObject);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            _P_Objects.Add(obj);
        }
    }
}


/* Use Pool
    
    [SerializeField]private ObjectPool _ObjectPool;

    private void Spawn() {
        for (int i = 0; i < _ObjectPool._P_Objects.Count; i++) {
            if (!_ObjectPool._P_Objects[i].activeInHierarchy) {
                _ObjectPool._P_Objects[i].transform.position = new Vector3(0,0,0);
                _ObjectPool._P_Objects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                _ObjectPool._P_Objects[i].SetActive(true);
                break;
            }
        }
    }
*/
