using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Vector2 GetRandomScreenPosition()
    {
        Vector2 ScreenSize = (Vector2)Camera.main.ScreenToWorldPoint(Vector2.zero) * new Vector2(-1f, -1f);
        return new Vector2(
            MathUtils.GetRandomReflectedInRange(ScreenSize.x),
            MathUtils.GetRandomReflectedInRange(ScreenSize.y)
        );
    }

    [Range(3, 30)]
    public int numberOfBots = 5;

    [Range(5, 20)]
    public int heroInitialSpeed = 5;

    [Range(5, 50)]
    public int heroInitialHealth = 5;

    [Range(5, 30)]
    public int initialNumberOfBullets = 10;

    public bool shouldBotFollowsTarget;

    public GameObject prefabBot;

    public GameObject prefabHero;

    GameObject _goHero;

    string _constUIStringNumberOfBots;

    void Awake()
    {
#if UNITY_EDITOR
        // Debug.Log("Unity Editor -> Run Tests");
        StringUtils.Test_GenerateFullName();
#endif
        _goHero = CreateHero();
        _constUIStringNumberOfBots = GameObject.Find("txtNumberOfBots").GetComponent<Text>().text;
        UpdateNumberOfBotsInUI(numberOfBots);
        CreateBots(_goHero.transform);
    }

    void CreateBots(Transform followTarget)
    {
        int counter = numberOfBots;
        GameObject containerForBots = new GameObject("ContainerBots");
        containerForBots.transform.SetParent(this.transform);
        while (counter-- > 0)
        {
            var goBot = Instantiate(prefabBot, containerForBots.transform);
            goBot.transform.position = Main.GetRandomScreenPosition();
            var goBotComponent = goBot.GetComponent<Bot>();
            if (shouldBotFollowsTarget)
            {
                goBotComponent.followTarget = followTarget;
            }
            goBotComponent.eventCollision.AddListener(onBotCollisionDetected);
        }
    }

    void onBotCollisionDetected(GameObject goCollider, GameObject target)
    {
        Debug.Log($"> Main -> onBotCollisionDetected: isHittedByBullet = {goCollider.name}");
        var bullet = goCollider.GetComponent<Bullet>();
        bool isHittedByBullet = bullet != null;
        if (isHittedByBullet)
        {
            // Debug.Log($"> Main -> onBotCollisionDetected: isHittedByBullet = {isHittedByBullet}");
            Destroy(target);
            Destroy(goCollider);
            UpdateNumberOfBotsInUI(--numberOfBots);
            // if (bullet.shouldDestroyBulletOnBotCollision(bot.GetComponent<Bot>())){
            // }
            if (numberOfBots == 0) {
                Debug.Log($"> Main -> You Win!");
            }
        }
    }
    
    void onHeroCollisionDetected(GameObject goCollider, GameObject target) {
        var bot = goCollider.GetComponent<Bot>();
        if (bot != null) {
            var goHeroComponent = _goHero.GetComponent<Hero>();
            goHeroComponent.health -= 1;
            if (goHeroComponent.health <= 0) {
                Debug.Log($"> Main -> Game Over!");
            }
        }
    }

    GameObject CreateHero()
    {
        var goHero = Instantiate(prefabHero);
        var goHeroComponent = goHero.GetComponent<Hero>();
        goHeroComponent.moveSpeed = heroInitialSpeed;
        goHeroComponent.health = heroInitialHealth;
        goHeroComponent.eventCollision.AddListener(onHeroCollisionDetected);
        return goHero; 
    }

    void UpdateNumberOfBotsInUI(int numberOfBots) {
        GameObject.Find("txtNumberOfBots").GetComponent<Text>().text = 
            _constUIStringNumberOfBots.Replace("$value", numberOfBots.ToString());
    }
}
