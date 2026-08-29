internal class WaterSource : WaterNode
{
  internal override bool IsOpen => true;

  internal override void Close() {}

  internal override bool IsEnabledBy(WaterNode waterNode) => false;
}
