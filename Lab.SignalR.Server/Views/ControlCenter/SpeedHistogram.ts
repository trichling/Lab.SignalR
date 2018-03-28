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
                      display: false
              }],
            yAxes : [{
                ticks: {
                    min: 0,
                    max: 10,
                    stepSize: 1
                }
            }]
          }
        })
    }
  });