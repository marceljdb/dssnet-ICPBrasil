/*
 * DSS - Digital Signature Services
 *
 * Copyright (C) 2011 European Commission, Directorate-General Internal Market and Services (DG MARKT), B-1049 Bruxelles/Brussel
 *
 * Developed by: 2011 ARHS Developments S.A. (rue Nicolas BovÃ© 2B, L-1253 Luxembourg) http://www.arhs-developments.com
 *
 * This file is part of the "DSS - Digital Signature Services" project.
 *
 * "DSS - Digital Signature Services" is free software: you can redistribute it and/or modify it under the terms of
 * the GNU Lesser General Public License as published by the Free Software Foundation, either version 2.1 of the
 * License, or (at your option) any later version.
 *
 * DSS is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License along with
 * "DSS - Digital Signature Services".  If not, see <http://www.gnu.org/licenses/>.
 */

using EU.Europa.EC.Markt.Dss.Signature;
using Sharpen;

namespace EU.Europa.EC.Markt.Dss.Signature
{
	/// <summary>Extend the level of AdES signature of a document.</summary>
	/// <remarks>
	/// Extend the level of AdES signature of a document. After level -EPES, going upper in the signature format level
	/// consists of adding usigned properties to the signature. It can be done without breaking the signature.
	/// </remarks>
	/// <version>$Revision: 1887 $ - $Date: 2013-04-23 14:56:09 +0200 (mar., 23 avr. 2013) $
	/// 	</version>
	public interface SignatureExtension
	{
		/// <summary>Extend the level of the signatures contained in a document.</summary>
		/// <remarks>Extend the level of the signatures contained in a document.</remarks>
		/// <param name="document"></param>
		/// <param name="originalData"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		/// <exception cref="System.IO.IOException">System.IO.IOException</exception>
		Document ExtendSignatures(Document document, Document originalData, SignatureParameters
			 parameters);

		/// <summary>Extend the level of one signature contained in a document.</summary>
		/// <remarks>Extend the level of one signature contained in a document. The signature is identified by 'signatureId'.
		/// 	</remarks>
		/// <param name="signatureId">The id of the signature to be extended.</param>
		/// <param name="document"></param>
		/// <param name="originalData"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		/// <exception cref="System.IO.IOException">System.IO.IOException</exception>
		Document ExtendSignature(object signatureId, Document document, Document originalData
			, SignatureParameters parameters);
	}
}