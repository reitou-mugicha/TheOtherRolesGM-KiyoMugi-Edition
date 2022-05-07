using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheOtherRoles.Objects
{
    class AssasinTrace {
        public static List<AssasinTrace> traces = new List<AssasinTrace>();

        private GameObject trace;
        private float timeRemaining;
        
        private static Sprite TraceSprite;
        public static Sprite getTraceSprite() {
            if (TraceSprite) return TraceSprite;
            TraceSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AssasinTraceW.png", 225f);
            return TraceSprite;
        }

        public AssasinTrace(Vector2 p, float duration=1f) {
            trace = new GameObject("AssasinTrace") { layer = 11 };
            trace.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
            //Vector3 position = new Vector3(p.x, p.y, PlayerControl.LocalPlayer.transform.localPosition.z + 0.001f); // just behind player
            Vector3 position = new Vector3(p.x, p.y, p.y / 1000f + 0.01f);
            trace.transform.position = position;
            trace.transform.localPosition = position;
            
            var traceRenderer = trace.AddComponent<SpriteRenderer>();
            traceRenderer.sprite = getTraceSprite();

            timeRemaining = duration;

            // display the assasins color in the trace
            float colorDuration = CustomOptionHolder.assasinTraceColorTime.getFloat();
            HudManager.Instance.StartCoroutine(Effects.Lerp(colorDuration, new Action<float>((p) => {
                Color c = Palette.PlayerColors[(int)Assasin.assasin.Data.DefaultOutfit.ColorId];
                if (Helpers.isLighterColor(Assasin.assasin.Data.DefaultOutfit.ColorId)) c = Color.white;
                else c = Palette.PlayerColors[6];
                //if (Camouflager.camouflageTimer > 0) {
                //    c = Palette.PlayerColors[6];
                //}

                Color g = Color.green; // Usual display color. could also be Palette.PlayerColors[6] for default grey like camo
                // if this stays black (0,0,0), it can ofc be removed.

                Color combinedColor = Mathf.Clamp01(p) * g + Mathf.Clamp01(1 - p) * c;

                if (traceRenderer) traceRenderer.color = combinedColor;
            })));

            float fadeOutDuration = 1f;
            if (fadeOutDuration > duration) fadeOutDuration = 0.5f * duration;
            HudManager.Instance.StartCoroutine(Effects.Lerp(duration, new Action<float>((p) => {
                float interP = 0f;
                if (p < (duration - fadeOutDuration) / duration)
                    interP = 0f;
                else interP = (p * duration + fadeOutDuration - duration) / fadeOutDuration;
                if (traceRenderer) traceRenderer.color = new Color(traceRenderer.color.r, traceRenderer.color.g, traceRenderer.color.b, Mathf.Clamp01(1 - interP));
            })));

            trace.SetActive(true);
            traces.Add(this);
        }

        public static void clearTraces() {
            traces = new List<AssasinTrace>();
        }

        public static void UpdateAll() {
            foreach (AssasinTrace traceCurrent in new List<AssasinTrace>(traces))
            {
                traceCurrent.timeRemaining -= Time.fixedDeltaTime;
                if (traceCurrent.timeRemaining < 0)
                {
                    traceCurrent.trace.SetActive(false);
                    UnityEngine.Object.Destroy(traceCurrent.trace);
                    traces.Remove(traceCurrent);
                }
            }
        }
    }
}