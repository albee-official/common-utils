using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitMaterialFeature : ScriptableRendererFeature {
    class RenderPass : ScriptableRenderPass {

        private string profilingName;
        private Material material;
        private int materialPassIndex;
        private RenderTargetIdentifier sourceID;
        
        [System.Obsolete]
        private RenderTargetHandle tempTextureHandle;

        public RenderPass(string profilingName, Material material, int passIndex) : base() {
            this.profilingName = profilingName;
            this.material = material;
            this.materialPassIndex = passIndex;
#pragma warning disable CS0612 // Type or member is obsolete
            tempTextureHandle.Init("_TempBlitMaterialTexture");
#pragma warning restore CS0612 // Type or member is obsolete
        }

        public void SetSource(RenderTargetIdentifier source) {
            this.sourceID = source;
        }

        [System.Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            CommandBuffer cmd = CommandBufferPool.Get(profilingName);

            RenderTextureDescriptor cameraTextureDesc = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDesc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempTextureHandle.id, cameraTextureDesc, FilterMode.Bilinear);
            Blit(cmd, sourceID, tempTextureHandle.Identifier(), material, materialPassIndex);
            Blit(cmd, tempTextureHandle.Identifier(), sourceID);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        public override void FrameCleanup(CommandBuffer cmd) {
#pragma warning disable CS0612 // Type or member is obsolete
            cmd.ReleaseTemporaryRT(tempTextureHandle.id);
#pragma warning restore CS0612 // Type or member is obsolete
        }
    }

    [System.Serializable]
    public class Settings {
        public Material material;
        public int materialPassIndex = -1; // -1 means render all passes
        public RenderPassEvent renderEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    [SerializeField]
    private Settings settings = new Settings();

    private RenderPass renderPass;

    public Material Material {
        get => settings.material;
    }

    public override void Create() {
        this.renderPass = new RenderPass(name, settings.material, settings.materialPassIndex);
        renderPass.renderPassEvent = settings.renderEvent;
    }

    [System.Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        renderPass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(renderPass);
    }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
}
