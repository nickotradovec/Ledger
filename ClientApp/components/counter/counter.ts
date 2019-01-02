import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class CounterComponent extends Vue {
    currentcount: number = 0;

    incrementCounter() {
        Vue.set(this, 'counter', 5);
        this.currentcount++;
    }
}
