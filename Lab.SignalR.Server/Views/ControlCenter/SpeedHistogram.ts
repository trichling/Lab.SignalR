import Vue from "vue";
import VueCharts from 'vue-chartjs'
import { Line, mixins } from 'vue-chartjs'

Vue.component('speed-histogram',  {
    extends: Line,
    mixins: [mixins.reactiveProp],
    props: ['chartData', 'options'],
    mounted () {
      (<any>this).renderChart(this.chartData, {
          responsive: true, 
          maintainAspectRatio: false,
          scales: {
              xAxes: [{
                  type: "time",
                  scaleLabel: {
                      display: true,
                      lableString: "Date"
                  }
              }],
            yAxes : [{
                scaleLabel: {
                    display: true,
                    lableString: "Date"
                }
            }]
          }
        })
    }
  });