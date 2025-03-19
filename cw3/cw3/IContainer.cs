namespace cw3;

public interface IContainer
{
    double ContainerWeight { get; set; }
    double Height { get; set; }
    double LoadWeight { get; set; }
    double Depth { get; set; }
    string SerialNum { get; set; }
    double MaximumLoad { get; set; }
    
    void Load(double weight) {}
    void Unload() {}
}