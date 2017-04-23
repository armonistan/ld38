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

        public enum PowerupType
        {
            None,
            Multiball,
            Slower,
            Faster,
            Shield,
            Pointmania
        }

        [Serializable]
        public class ObstacleSprites
        {
            public PowerupType PowerupType;
            public Sprite FullSprite;
            public Sprite TwoThirdsSprite;
            public Sprite OneThirdSprite;
        }

        public PowerupType CurrentPowerupType;
        public ObstacleSprites[] Sprites;

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;

        public void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        public void Update()
        {
            var currentSprite = Sprites.First(sprites => sprites.PowerupType == CurrentPowerupType);

            switch (State)
            {
                case States.Full:
                    _spriteRenderer.sprite = currentSprite.FullSprite;
                    break;
                case States.TwoThirds:
                    _spriteRenderer.sprite = currentSprite.TwoThirdsSprite;
                    break;
                case States.OneThird:
                    _spriteRenderer.sprite = currentSprite.OneThirdSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _circleCollider.radius = _spriteRenderer.sprite.bounds.size.x / 2;
        }
    }
}
