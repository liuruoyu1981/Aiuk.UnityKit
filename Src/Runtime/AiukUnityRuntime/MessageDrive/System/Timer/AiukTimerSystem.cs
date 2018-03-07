//namespace AiukUnityRuntime.Timer
//{
//    public class AiukTimerSystem :
//        AiukAbsSystem<IAiukTimer, IAiukTimerVisitor>,
//        IAiukTimerVisitor
//    {
//        public override void Execute()
//        {
//        }

//        public override void LateExecute()
//        {
//        }

//        public override void FixedExecute()
//        {
//            UpdateTimer();
//        }

//        private void UpdateTimer()
//        {
//            var timer = VisitMediator.NextData(this);
//            if (timer == null) return;

//            timer.Update();
//        }

//        public override void FinishExecute()
//        {
//            base.FinishExecute();
//        }
//    }
//}
