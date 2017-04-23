using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObstacleControl : StatefulMonoBehavior<ObstacleControl.States>
    {
        public enum States
        {
            Full,
            TwoThirds,
            OneThird
        }

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;

        public Sprite[] Sprites;

        public void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        public void Update()
        {
            _spriteRenderer.sprite = Sprites[(int)State];
            _circleCollider.radius = _spriteRenderer.sprite.bounds.size.x / 2;
        }
    }
}
