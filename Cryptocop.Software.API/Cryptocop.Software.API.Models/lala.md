AddressInputModel
•StreetName* (string)
•HouseNumber* (string)
•ZipCode* (string)
•Country* (string)
•City* (string)

• LoginInputModel
•Email* (string)
•Must be a valid email address
•Password* (string)
•A minimum length of 8 characters

• OrderInputModel
•AddressId (int)
•PaymentCardId (int)

•PaymentCardInputModel
•CardholderName* (string)
•A minimum length of 3 characters
•CardNumber* (string)
•Must be a valid credit card number
•Month (int)
•The range for this number is an inclusive 1 to 12
•Year (int)
•The range for this number is an inclusive 0 to 99

• RegisterInputModel
•Email* (string)
•Must be a valid email address
•FullName* (string)
•A minimum length of 3 characters
•Password* (string)
•A minimum length of 8 characters
•PasswordConfirmation* (string)
•A minimum length of 8 characters
•Must be the same value as the property Password

• ShoppingCartItemInputModel
•ProductIdentifier* (string)
•Quantity* (nullable float)
•The range for this number is an include 0.01 to the float type maximum value