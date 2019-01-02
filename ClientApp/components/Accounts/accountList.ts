import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Account {
    AccountId: number;
    InitialAccountBalance: number;
    Commence: string;
    Cease: string;
    AccountName: string;  
}

@Component
export default class AccountsManagement extends Vue {
    accountlist: Account[] = [];

    mounted() {

        fetch('api/Accounts/AccountsList')
            .then(response => JSON.stringify(response.json()))
            //.then(response => response.json() as Promise<Account[]>)
            .then(data => {
                //this.accountlist = data;
                //data.forEach(function <Account>(act) {
                //    var account: Account;
                //    account.name = act.name;
                //})
            });      
    }
}
