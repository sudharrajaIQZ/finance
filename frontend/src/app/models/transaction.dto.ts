import { IsNotEmpty, IsString, IsNumber, IsOptional, isNotEmpty } from "class-validator";
export class TransactionDto {
  @IsNotEmpty()
  @IsString()
  customerName?: string;

  @IsNotEmpty()
  @IsString()
  customerId?: string;

  @IsNotEmpty()
  @IsString()
  type?: string;

  @IsNotEmpty()
  @IsNumber()
  amount!: number;

  @IsOptional()
  transactionDate?: Date;

  @IsNotEmpty()
  @IsString()
  userId!: string;

}

// export class TransactionDto {
//     customerName: string ;
//     customerId: string;
//     type: string;
//     amount: number;
//     transactionDate?: Date;
// }

