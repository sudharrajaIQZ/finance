import { IsNotEmpty, IsPhoneNumber } from 'class-validator';

export class CustomerDto {
    id?: string;

    @IsNotEmpty()
    firstName: string = "";

    @IsNotEmpty()
    lastName!: string;

    @IsPhoneNumber()
    phoneNumber!: string;

    @IsNotEmpty()
    address!: string;

    @IsNotEmpty()
    gender!: string;

    dob?: Date;

    @IsNotEmpty()
    bankId!: string;

    openingDate?: Date;

    balance: number = 0;

    @IsNotEmpty()
    accountType!: string;

    @IsNotEmpty()
    userId!: string;
    
    constructor(){
        this.userId = ""
    }
}