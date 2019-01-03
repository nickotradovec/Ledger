import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Account {
    AccountId: number;
    InitialAccountBalance_Formatted: string;
    Commence_Formatted: string;
    Cease: Date;
    AccountName: string;
}

@Component
export default class AccountsManagement extends Vue {
    accountlist: Account[] = [];

    mounted() {
        fetch('api/Accounts/AccountsList')
            .then(response => response.json() as Promise<Account[]>)
            .then(data => {
                this.accountlist = data;
            });
    }
}
