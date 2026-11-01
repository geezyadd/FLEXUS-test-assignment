namespace Features.VfxModule.Scripts
{
    public readonly struct VfxHandle
    {
        private readonly VfxInstance _instance;
        private readonly int _generation;

        public VfxHandle(VfxInstance instance)
        {
            _instance = instance;
            _generation = instance.Generation;
        }

        public void Stop()
        {
            if (_instance != null && _instance.Generation == _generation)
                _instance.Stop();
        }
    }
}
