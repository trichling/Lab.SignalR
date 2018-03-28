import Machine from "../Machines/Machine";

export default class MachineStatus {

    name : string = ""; 
    chartData = {}
    speedHistory : number[] = [];

    constructor(public machine : Machine) {
        this.name = machine.name;
    }

    pushSpeed(speed : number) {
        this.machine.speedMeterPerSecond = speed;
        this.speedHistory.push(speed);
        
        this.chartData = {
            labels: this.speedHistory.map((v, i) => i),
            datasets: [
                {
                    label: 'Geschwindigkeitsverlauf',
                    backgroundColor: '#f87979',
                    data: this.speedHistory
                }
            ]
        };
        

    }

}