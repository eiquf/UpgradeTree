using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EditorFlowerAnimation
{
    private List<FlowerParticle> _flowers = new();
    private double _lastUpdateTime;

    private struct FlowerParticle
    {
        public Vector2 position;
        public Vector2 velocity;
        public string emoji;
        public float lifetime;
        public float maxLifetime;
        public float rotation;
        public float rotationSpeed;
    }

    private static readonly string[] FlowerEmojis = { "🌸", "🌺", "🌷", "🌻", "💮", "🏵️", "💐", "🌼", "🪻", "🪷" };

    /// <summary>
    ///  Spawn _flowers
    /// </summary>
    public void Spawn(Vector2 position, int count = 10)
    {
        for (int i = 0; i < count; i++)
        {
            _flowers.Add(new FlowerParticle
            {
                position = position,
                velocity = new Vector2(
                    UnityEngine.Random.Range(-100f, 100f),
                    UnityEngine.Random.Range(-150f, -50f)
                ),
                emoji = FlowerEmojis[UnityEngine.Random.Range(0, FlowerEmojis.Length)],
                lifetime = 0,
                maxLifetime = UnityEngine.Random.Range(1f, 2f),
                rotation = UnityEngine.Random.Range(0f, 360f),
                rotationSpeed = UnityEngine.Random.Range(-180f, 180f)
            });
        }


    }

    /// <summary>
    /// UpdateAndDraw_flowers over UI.
    /// </summary>
    public void UpdateAndDraw_flowers(double lastTime)
    {
        if (_flowers.Count == 0) return;
        _lastUpdateTime = lastTime;

        var currentTime = EditorApplication.timeSinceStartup;
        var deltaTime = (float)(currentTime - _lastUpdateTime);
        _lastUpdateTime = currentTime;

        deltaTime = Mathf.Min(deltaTime, 0.1f);

        var _flowerstyle = new GUIStyle
        {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter
        };

        for (int i = _flowers.Count - 1; i >= 0; i--)
        {
            var flower = _flowers[i];

            flower.position += flower.velocity * deltaTime;
            flower.velocity.y += 200f * deltaTime;
            flower.lifetime += deltaTime;
            flower.rotation += flower.rotationSpeed * deltaTime;

            var alpha = 1f - (flower.lifetime / flower.maxLifetime);

            if (flower.lifetime >= flower.maxLifetime)
            {
                _flowers.RemoveAt(i);
                continue;
            }

            _flowers[i] = flower;

            var matrix = GUI.matrix;
            GUIUtility.RotateAroundPivot(flower.rotation, flower.position);

            var oldColor = GUI.color;
            GUI.color = new Color(1, 1, 1, alpha);
            GUI.Label(new Rect(flower.position.x - 10, flower.position.y - 10, 20, 20), flower.emoji, _flowerstyle);
            GUI.color = oldColor;

            GUI.matrix = matrix;
        }

        if (_flowers.Count > 0)
            InternalEditorUtility.RepaintAllViews();
    }

}