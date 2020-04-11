/* tslint:disable */


	/**
	 * Třída popisuje validační chybu WebAPI.
	 */
	export interface ValidationError {

		/**
		 * Chyby validace modelu.
            Null hodnota není do JSON serializována.
            Je vzájemně výlučné s Message. Buď je jedno, nebo druhé.
		 */
		errors: FieldValidationError[];

		/**
		 * Text chyby. Použito pro chyby vyhozené "ručně" výjimkou OperationFailedException (ev. jiné).
            Null hodnota není do JSON serializována.
            Je vzájemně výlučné s Errors. Buď je jedno, nebo druhé.
		 */
		message: string;

		/**
		 * Status code.
		 */
		statusCode: number;
	}
	export interface FieldValidationError {
		field: string;
		message: string;
	}
