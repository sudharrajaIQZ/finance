// import { IsNotEmpty, IsString, IsNumber, IsOptional } from "class-validator";
// export interface TransactionDto {
//     @IsNotEmpty()
//     @IsString()
//     customerName?: string;
  
//     @IsNotEmpty()
//     @IsString()
//     customerId?: string;
  
//     @IsNotEmpty()
//     @IsString()
//     type?: string;
  
//     @IsNotEmpty()
//     @IsNumber()
//     amount!: number;
  
//     @IsOptional()
//     transactionDate?: Date;
  
//     @IsNotEmpty()
//     @IsString()
//     userId!: string;

//   }
  
export interface TransactionDto {
    customerName: string ;
    customerId: string;
    type: string;
    amount: number;
    transactionDate?: Date;
}

